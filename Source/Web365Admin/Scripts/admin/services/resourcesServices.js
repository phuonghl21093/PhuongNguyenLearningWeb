//create by BienLV2
//15-07-2014
//services use to other services
'use strict';
web365app.factory('resourcesServices', ['$q', '$templateCache', '$http', function ($q, $templateCache, $http) {

    var resourcesServices = {};

    //function get list product type
    var _getList = function (obj) {

        var deferred = $q.defer();

        //get data from services
        $http.get('/Setting/GetList', {
            params: obj
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _getPropertyFilter = function () {

        var deferred = $q.defer();

        //get data from services
        $http.get('/Setting/GetPropertyFilter').success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    //function get detail form picture type
    var _getDetailForm = function (id) {

        var deferred = $q.defer();

        //get data from services
        $http.get('/Setting/Detail', {
            params: {
                id: id
            }
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    //function get edit form product type
    var _getEditForm = function (id, lang) {

        var deferred = $q.defer();

        //get data from services
        $http.get('/Setting/EditForm', {
            params: {
                id: id,
                language: lang
            }
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    //function get edit form product type
    var _modified = function (obj) {

        var deferred = $q.defer();

        //get data from services
        $http.post('/Setting/Action', obj, {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _showAll = function (listId) {

        var deferred = $q.defer();

        $http.post('/Setting/ShowAll', {
            listId: listId
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _hideAll = function (listId) {

        var deferred = $q.defer();

        $http.post('/Setting/HideAll', {
            listId: listId
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _deleteAll = function (listId) {

        var deferred = $q.defer();

        $http.post('/Setting/Delete', {
            listId: listId
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _nameExist = function (id, name) {

        var deferred = $q.defer();

        $http.post('/Setting/NameExist', {
            id: id,
            name: name
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    resourcesServices.getList = _getList;
    resourcesServices.getPropertyFilter = _getPropertyFilter;
    resourcesServices.getDetailForm = _getDetailForm;
    resourcesServices.getEditForm = _getEditForm;
    resourcesServices.modified = _modified;
    resourcesServices.showAll = _showAll;
    resourcesServices.hideAll = _hideAll;
    resourcesServices.deleteAll = _deleteAll;
    resourcesServices.nameExist = _nameExist;

    return resourcesServices;
}]);