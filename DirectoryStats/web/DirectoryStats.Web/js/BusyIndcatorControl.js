// BusyIndcatorControl.js
(function () {
  "use strict";

    angular.module("BusyIndcatorControl", [])
    .directive("waitCursor", waitCursor);

  function waitCursor() {
    return {
      scope: {
        show: "=displayWhen"
      },
      restrict: "E",
      templateUrl: "/views/waitCursor.html"
    };
  }

})();