//Login User
shopApp.controller("LoginController", ['$scope', 'userFactory', '$route',
    function ($scope, userFactory, $route) {

        $scope.form = {
            email: $('#email').val(),
            password: $("#password").val()
        }

        $scope.save = function (form) {

            userFactory.login(form)
                .success(function (data, status) {
                    window.location = "/search";
                })
                .error(function (data, status) {
                    if (status === 400) {
                        $scope.alert = {
                            type: 'danger',
                            message: data.Message,
                            display: true
                        };
                    } else {
                        $scope.alert = {
                            type: 'danger',
                            title: 'Oh Crap!',
                            message: 'There was an error while processing your request.',
                            display: true
                        };
                    }
                });

        }

    }]);

//Register Account
shopApp.controller("RegisterController", ['$scope', 'userFactory',
    function ($scope, userFactory) {
        var errorMsg = "";
        $scope.save = function (form) {

            form.Mobile = form.PhonePrefix + form.PhoneNo;

            userFactory.register(form)
                .success(function (data) {
                    $scope.alert = {
                        type: 'success',
                        display: true,
                        message: 'Thank you, your message have been successfully sent out.'
                    };
                })
                .error(function (data, status) {
                    if (status === 400) {
                        $scope.alert = {
                            type: 'danger',
                            display: true,
                            message: data.Message
                        };

                    } else {
                        $scope.alert = {
                            type: 'danger',
                            message: 'There was an error while processing your request.',
                            display: true
                        };
                    }
                });

        }


    }]);

//User
shopApp.controller("UserController", ['$scope', 'userFactory', '$route',
    function ($scope, userFactory, $route) {

        $scope.logout = function () {
            userFactory.logout()
                .success(function (data) {
                    window.location = "/user/login";
                });
        }

    }]);