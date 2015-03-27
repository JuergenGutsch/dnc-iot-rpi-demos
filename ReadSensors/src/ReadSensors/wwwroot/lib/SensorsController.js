(function () {
    'use strict';

    angular
        .module('GpioDemoApp')
        .controller('SensorsController', sensorsController);

    sensorsController.$inject = ['$http', '$scope'];

    function sensorsController($http, $scope) {
        /* jshint validthis:true */
        var sensors = $.connection.sensors;
        $.extend(sensors.client, {
            enabled: function () {
                enabled();
            },
            showessage: function (message) {
                showessage(message);
            },
            disabled: function () {
                disabled();
            }
        });

        $.connection.hub.start()
            .then(init);

        function init() {

        }

        var vm = this;
        vm.name = "Hallo Gpio";
        vm.status = "disabled";
        vm.data = {
            X: 0,
            Y: 0,
            Z: 0,
            Width: 0
        };
        vm.enable = enable;
        vm.disable = disable;

        function enable() {
            sensors.server.enable($.connection.hub.id);
        }
        function disable() {
            sensors.server.disable($.connection.hub.id);
        }

        function enabled() {
            vm.status = "enabled";
            if (!$scope.$$phase) {
                $scope.$apply();
            }
        }
        function showessage(message) {
            vm.data = message;
            if (!$scope.$$phase) {
                $scope.$apply();
            }
        }
        function disabled() {
            vm.status = "disabled";
            if (!$scope.$$phase) {
                $scope.$apply();
            }
        }

        activate();

        function activate() {


        }
    }
})();
