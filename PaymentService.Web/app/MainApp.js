
var app = angular.module('cardPayment', ['ngRoute', 'ngMessages']);

//This configures the routes and associates each route with a view and a controller
app.config(function ($routeProvider) {
    $routeProvider
        .when('/confirm',
            {
                templateUrl: 'app/views/confirm-payment.html'
            })
        .when('/payment',
            {
                controller: 'CardPaymentController',
                templateUrl: 'app/views/card-payment.html'
            })
        .otherwise({ redirectTo: '/payment' });
});

