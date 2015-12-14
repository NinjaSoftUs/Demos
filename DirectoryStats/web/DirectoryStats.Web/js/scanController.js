// scanController.js
(function () {
    "use strict";

    // Getting the existing module
    angular.module("app-scan")
      .controller("scanController", scanController);

    function scanController($http) {
        var vm = this;

        vm.results = "";

        //vm.TotalFiles = "";
        //vm.TotalFiles = "";
        //vm.TotalFolders = "";

        vm.errorMessage = "";

        vm.path1 = "";
        vm.path2 = "";
        vm.path3 = "";

        vm.isBusy = false;

        vm.runScan = function () {
            vm.isBusy = true;
            vm.results = "";
            $http({
                url: "/home/RunScan",
                method: "GET",
                params: {
                    path1: vm.path1,
                    path2: vm.path2,
                    path3: vm.path3
                }
            })
            .then(function (response) {
                // Success
                vm.errorMessage = "";
                vm.results = response.data;

              //  vm.totalfiles = response.data.to
               // alert(vm.results);
            }, function (error) {
                // Failure
                vm.ererrorMessage = "Field to run: " + error;
            }).finally(function () {
                vm.isBusy = false;
            });
        };

       
    }
})();