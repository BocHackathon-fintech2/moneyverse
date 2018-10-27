'use strict';
/**
 * @ngdoc function
 * @name sbAdminApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the sbAdminApp
 */
angular.module('sbAdminApp')
    .controller('addBankCtrl', ['$scope', '$http', function ($scope, $http) {
        $http.get('Boc/AuthUrl').then(function (response) {
            $scope.url = response.data.url;
        })
    }]
);