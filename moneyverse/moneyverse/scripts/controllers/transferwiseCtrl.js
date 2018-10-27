'use strict';
/**
 * @ngdoc function
 * @name sbAdminApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the sbAdminApp
 */
angular.module('sbAdminApp')
    .controller('transferwiseCtrl', ['$scope', '$http', '$interval', function ($scope, $http, $interval) {
        $scope.title = 'Login with Transferwise'
        $scope.state = 'login';
        $scope.url = 'https://sandbox.transferwise.tech/oauth/authorize?response_type=code&client_id=sapiens&redirect_uri=http://localhost:50033/Transferwise/VerifyCode'
    }]
);