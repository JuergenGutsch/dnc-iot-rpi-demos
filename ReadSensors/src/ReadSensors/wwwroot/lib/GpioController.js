(function () {
    'use strict';

    angular
        .module('GpioDemoApp')
        .controller('GpioController', GpioController);

    GpioController.$inject = ['$http']; 

    function GpioController($http) {
        /* jshint validthis:true */
        var vm = this;
        vm.name = "Hallo Gpio";
        vm.switchGpio11On = switchGpio11On;
        vm.switchGpio11Off = switchGpio11Off;

        function switchGpio11On() {
            $http.post('/Gpio/Gpio11On').success(function (data) {
                alert(data);
            });
        }
        function switchGpio11Off() {
            $http.post('/Gpio/Gpio11Off').success(function (data) {
                alert(data);
            });
        }

        activate();

        function activate() {
            

        }
    }
})();
