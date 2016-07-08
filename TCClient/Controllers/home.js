(function () {
    'use strict';

    var turnerChallenge = angular.module('turnerApp');
    turnerChallenge.controller('homeController', search);
    turnerChallenge.controller('detailController', getDetails);

    function search($scope, $http) {

        //Used to display the data
        $http.get('/Home/Search/').success(function (data) {
            $scope.Titles = data;
        });

        $scope.displayDetails = function (Details){
            $scope.Details = angular.fromJson(Details);
        }
        $scope.parseDetails = function () {
            var stack = { text: "" };
            var depth = 0;
            $('#details').empty();
            iterateDetails($scope.Details, stack, depth);
        }
    }
    function getDetails($scope, $http) {
        $scope.ShowDetails = function () {
            var location = '/Home/Details/' + $scope.title.TitleId;
            $http.get('/Home/Details/' + $scope.title.TitleId).success(function (data) {
                $scope.displayDetails(data);
            });
        }
    }
})();
function iterateDetails(obj, stack, depth) {
    for (var property in obj) {
        if (obj.hasOwnProperty(property)) {
            if (Array.isArray(obj[property])) {
                stack.text += '<div style="padding-left:10px">' + property + ' ';
                iterateDetails(obj[property], stack, depth + 1);
            } else if (typeof obj[property] == "object") {
                iterateDetails(obj[property], stack, depth);
            } else {
                console.log(property + "   " + obj[property]);
                stack.text += '<div style="padding-left:10px">' + property + " : " + obj[property] + '</div>';
            }
        }
    }

    stack.text += '</div>';
    if (depth == 1)
    {
        //$('#details').append($("<div/>").html(stack));
        $('#details').append(stack.text);
        stack.text = "";
    }
}

//function iterateDetails(obj, stack, depth) {
//    for (var property in obj) {
//        if (obj.hasOwnProperty(property)) {
//            if (Array.isArray(obj[property])) {
//                iterateDetails(obj[property], stack + '<div style="padding-left:10px">' + property + ' ', depth+1);
//            } else if (typeof obj[property] == "object") {
//                iterateDetails(obj[property], stack, depth);
//            } else {
//                console.log(property + "   " + obj[property]);
//                stack += '<div style="padding-left:10px">' + property + " : " + obj[property] + '</div>';
//            }
//        }
//    }
//    stack += '</div>';
//    //if (depth == 1)
//    {
//        //$('#details').append($("<div/>").html(stack));
//        $('#details').append(stack);
//    } 
//}
//(function () {
//    'use strict';

//    angular
//        .module('turnerApp')
//        .controller('detailController', search);

//    }
//})();
