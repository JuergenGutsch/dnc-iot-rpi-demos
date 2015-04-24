(function () {
    'use strict';

    angular
        .module('GpioDemoApp')
        .controller('SensorsController', sensorsController);

    sensorsController.$inject = ['$http', '$scope', '$interval'];

    function sensorsController($http, $scope, $interval) {
        /* jshint validthis:true */
       

        var vm = this;
        vm.name = "Hallo Gpio";
        vm.status = "disabled";
        vm.data = {
            x: 0,
            y: 0,
            z: 0,
            width: 0,
            status
        };
        vm.enable = enable;
        vm.disable = disable;

        var stop;
        function enable() {
            stop = $interval(startMeasure, 500);
        }

        function disable() {
            if (angular.isDefined(stop)) {
                $interval.cancel(stop);
                stop = undefined;
            }
        }

        function startMeasure() {
            $http
                .get('/Gpio/GetDistance')
                .success(function(data) {
                    vm.data.width = data.Width;
                    vm.data.status = data.ServerStatus;
                });
        }
    }
})();
