(function () {
    'use strict';

    app.controller('CardPaymentController', function ($scope, $location,$http) {
        $scope.currentYear = new Date().getFullYear();
        $scope.currentMonth = new Date().getMonth() + 1;

        $scope.submitForm = function () {
            $scope.paymentForm.cardnumber.$setValidity("ValidCardNumnber", true);

            if ($scope.paymentForm.$valid) {
                var getUrl = "http://localhost:52604/api/paymentservice/IsValidCardNumber/" + $scope.cardnumber + "/" + $scope.cardtype;
                $http({
                    method: 'GET',
                    url: getUrl,
                    data: {}
                }).success(function (data, status, headers, config) {
                    if (!data)
                        $scope.paymentForm.cardnumber.$setValidity("ValidCardNumnber", false);
                    else
                        $location.path('confirm');
                }).error(function (data, status, headers, config) {
                    $scope.paymentForm.cardnumber.$setValidity("ValidCardNumnber", false);
                });                
            }
        };
    });

    app.directive('cardExpiryMonth', function () {
        return {
            require: 'ngModel',
            link: function (scope, elem, attrs, ctrl) {
                scope.$watch('[expirymonth]', function (value) {
                    ctrl.$setValidity('ValidMonth', true);

                    if (!(parseInt(scope.expirymonth, 10) >= 1 && parseInt(scope.expirymonth, 10) <= 12)) {
                        ctrl.$setValidity('ValidMonth', false)
                    }

                    return value
                }, true);
            }
        }
    }
  );

    app.directive('cardExpiryYear', function () {
        return {
            require: 'ngModel',
            link: function (scope, elem, attrs, ctrl) {
                scope.$watch('[expirymonth, expiryyear]', function (value) {
                    ctrl.$setValidity('ValidYear', true);
                    ctrl.$setValidity('ValidExpiry', true);

                    if (scope.expiryyear < scope.currentYear) {
                        ctrl.$setValidity('ValidYear', false)
                    }
                    else {
                        if ((parseInt(scope.expirymonth, 10) >= 1 && parseInt(scope.expirymonth, 10) <= 12)) {
                            if (scope.expiryyear == scope.currentYear && parseInt(scope.expirymonth, 10) <= scope.currentMonth) {
                                ctrl.$setValidity('ValidExpiry', false)
                            }
                        }
                    }

                    return value
                }, true);
            }
        }
    }
    );

})();
