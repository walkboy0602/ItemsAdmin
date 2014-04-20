//Tutorial of how to use Angular integrate with Web Form
//http://weblogs.asp.net/dwahlin/archive/2013/08/16/using-an-angularjs-factory-to-interact-with-a-restful-service.aspx

angular.module('shopAPI', [])
    .factory('userFactory', ['$http', function ($http) {
        return {
            register: function (data) {
                return $http.post('/api/user/register', data);
            },
            login: function (data) {
                return $http.post('/api/user/login', data);
            },
            logout: function (data) {
                return $http.post('/api/user/logout', data);
            }
        }
    }])
    .factory('ReferenceFactory', ['$http', function ($http) {

        return {
            list: function (data) {
                return $http.get('/reference/get', { params: { ReferenceType: data, json: true } });
            }
        }

    }])
    .factory('ListingFactory', ['$http', function ($http) {

        return {
            getImage: function (id) {
                return $http.get('/listing/getImage', { params: { id: id } });
            },
            changeDesc: function (data) {
                return $http.post('/listing/changeDesc', data);
            },
            deleteImage: function (data) {
                return $http.post('/listing/deleteImage', data);
            },
            getSpecification: function (id) {
                return $http.get('/listing/getSpecification', { params: { ListingID: id } });
            },
            saveSpecification: function (data) {
                return $http.post('/listing/saveSpecification', data);
            },
            saveDescription: function (data) {
                return $http.post('/listing/saveDescription', data);
            },
            getDescription: function (id) {
                return $http.get('/listing/getDescription', { params: { ListingID: id } });
            },
            saveOption: function(data)
            {
                return $http.post('/listing/saveOption', data);
            },
            getOption: function (id)
            {
                return $http.get('/listing/getOption', { params: { ListingID: id } });
            },
            editOption: function(data)
            {
                return $http.post('/listing/editOption', data);
            },
            deleteOption: function(data)
            {
                return $http.post('/listing/deleteOption', data);
            }
        }


    }])
    .factory('ImageFactory', ['$http', function ($http) {

        return {
            delete: function (data) {
                //console.log(JSON.stringify(data));
                return $http.post('/api/image/delete', data);
            }
        }

    }]);


