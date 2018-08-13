var app = angular.module('AdvertisementModule', []);
app.controller('CreateController', ['$scope', '$location', '$rootScope', function ($scope, $location, $rootScope) {

    // api
    var getAdvertisementCreate = '/Advertisement/GetAdvertisementCreate/';
    var saveBuyAdvertisement = '/Advertisement/SaveBuyAdvertisement/';

    // Status code
    var advertisementType = {
        Sell: 1,
        Buy: 2
    };

    // scope
    $scope.TextDetail = 'Cơ bản';
    $scope.IsDetail = false;
    $scope.AdvertisementType = advertisementType.Buy;

    // UsdRate
    $scope.Id = $('#adsid').val();
    $scope.UsdRate = -1;
    $scope.IsValidUsdRate = false;
    var minUsdRate = 15000;
    var maxUsdRate = 50000;

    $scope.ReferenceExchange = -1;
    $scope.BitcoinPriceMaker = -1;
    $scope.Fee = -1;
    $scope.BitcoinPriceTaker = -1;
    $scope.ListCurrency = [];
    $scope.CurrencySelected = null;

    // CoinPriceLimit
    $scope.CoinPriceLimit = -1;
    $scope.IsValidCoinPriceLimit = false;

    // MinAmount
    $scope.MinAmount = -1;
    $scope.MinMinAmount = -1;
    $scope.MaxMinAmount = 10;

    // MaxAmount
    $scope.MaxAmount = -1;
    //$scope.MinMaxAmount = -1;

    $scope.ListCountry = [];
    $scope.CountrySelected = null;

    $scope.anhien1 = true;
    $scope.showTwo = false;
    $scope.showThree = false;
    $scope.showFour = false;
    $scope.showFive = false;
    $scope.showSix = false;
    $scope.showSeven = false;
    $scope.showEight = false;

    $scope.changeOne = function () {
        $scope.anhien1 = !$scope.anhien1;
    }

    $scope.changeTwo = function () {
        $scope.showTwo = !$scope.showTwo;
    }

    $scope.changeThree = function () {
        $scope.showThree = !$scope.showThree;
    }

    $scope.changeFour = function () {
        $scope.showFour = !$scope.showFour;
        $scope.CoinPriceLimit = 0;
    }

    $scope.changeFive = function () {
        $scope.showFive = !$scope.showFive;
    }

    $scope.changeSix = function () {
        $scope.showSix = !$scope.showSix;
    }

    $scope.changeSeven = function () {
        $scope.showSeven = !$scope.showSeven;
    }

    $scope.changeEight = function () {
        $scope.showEight = !$scope.showEight;
    }

    $scope.changeIsDetail = function () {
        //$scope.anhien = ($scope.hide_show = !$scope.hide_show) ? "Chi tiết" : "Cơ bản";
        $scope.IsDetail = !$scope.IsDetail;
        $scope.TextDetail = ($scope.IsDetail) ? "Cơ bản" : "Chi tiết";
    }

    $scope.changeUsdRate = function () {
        if ($scope.UsdRate >= minUsdRate && $scope.UsdRate <= maxUsdRate) {
            $scope.BitcoinPriceMaker = $scope.ReferenceExchange * $scope.UsdRate;
            $scope.BitcoinPriceTaker = $scope.BitcoinPriceMaker - ($scope.BitcoinPriceMaker * $scope.Fee);

            $scope.IsValidUsdRate = true;
        } else {
            $scope.IsValidUsdRate = false;
        }
    }

    $scope.changeCoinPriceLimit = function () {
        var minCoinPriceLimit = $scope.BitcoinPriceTaker * 0.5;
        var maxCoinPriceLimit = $scope.BitcoinPriceTaker * 1.2;

        if ($scope.CoinPriceLimit >= minCoinPriceLimit && $scope.CoinPriceLimit <= maxCoinPriceLimit) {
            $scope.BitcoinPriceMaker = $scope.BitcoinPriceTaker = $scope.CoinPriceLimit;

            $scope.IsValidCoinPriceLimit = true;
        } else {
            $scope.IsValidCoinPriceLimit = false;
        }
    }

    $scope.changeCurrency = function () {
        //alert($scope.CurrencySelected.currencyCode);
    }

    $scope.saveBuyAdvertisement = function () {
        if (!$scope.IsValidUsdRate) {
            showErrorToastr('bitUSD not valid');
            return false;
        }

        if ($scope.CoinPriceLimit !== -1 && !$scope.IsValidCoinPriceLimit) {
            showErrorToastr('Coin max price not valid');
            return false;
        }

        if ($scope.MinAmount < $scope.MinMinAmount || $scope.MinAmount > $scope.MaxMinAmount) {
            showErrorToastr('Min amount not valid');
            return false;
        }

        if ($scope.MaxAmount < $scope.MinAmount || $scope.MaxAmount > $scope.MaxMinAmount) {
            showErrorToastr('Max amount not valid');
            return false;
        }

        var model = new Object();

        if ($scope.Id > 0) {
            model.Id = $scope.Id;
        } else {
            
        }

        model.AdvertisementType = $scope.AdvertisementType;
        model.CurrencyId = $scope.CurrencySelected.id;
        //model.ECurrencyId = xxx; // check trong resource
        model.BitUSDPrice = $scope.UsdRate;
        model.BitcoinPriceMaker = $scope.BitcoinPriceMaker;
        model.BitcounPriceTaker = $scope.BitcoinPriceTaker;
        
        model.CoinPriceLimit = $scope.CoinPriceLimit;
        model.CountryId = $scope.CountrySelected.id;

        model.MinAmount = $scope.MinAmount;
        //model.MinMinAmount = $scope.MinAmount;

        model.MaxAmount = $scope.MaxAmount;

        model.ReferenceExchange = $('#ReferenceExchange').val();
        model.PaymentMethod = $('#PaymentMethod').val();
        //model.RejectUserNotVeryfied = xxx; // ben tab ban
        model.PaymentTime = $('#PaymentTime').val();

        model.BankId = $scope.BankSelected.id;

        Loading.Show();
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            url: saveBuyAdvertisement,
            data: model,
            dataType: 'json',
            //type: "POST",
            cache: false,
            success: function (data) {
                if (data !== null) {
                    if (data.code === statusCode.Success) {
                        showSuccessToastr(data.message);

                        window.location = "/advertisement/view/" + data.data.id + "?m=success";

                    } else {
                        showErrorToastr(data.message);
                    }
                }

                // apply
                $scope.$apply();
                Loading.Hide();
            }
        });
    };

    $scope.changeAdvertisementType = function(type) {
        $scope.AdvertisementType = type;
    }

    var loadData = function (id = 0) {
        Loading.Show();

        $.ajax({
            contentType: 'application/json; charset=utf-8',
            url: getAdvertisementCreate,
            data: { id: id },
            dataType: 'json',
            type: "GET",
            cache: false,
            success: function (data) {
                if (id === 0) // new
                {
                    $scope.UsdRate = data.usdRate;
                    $scope.IsValidUsdRate = true;

                    $scope.ReferenceExchange = data.referenceExchange;
                    $scope.Fee = data.fee;

                    $scope.BitcoinPriceMaker = $scope.ReferenceExchange * $scope.UsdRate;
                    $scope.BitcoinPriceTaker = $scope.BitcoinPriceMaker - ($scope.BitcoinPriceMaker * $scope.Fee);

                    $scope.TextDetail = ($scope.IsDetail) ? "Cơ bản" : "Chi tiết";

                    // List currency
                    $scope.ListCurrency = data.listCurrency;
                    $scope.CurrencySelected = data.listCurrency[0];

                    // Min,max amount
                    $scope.MinAmount = data.minAmount;
                    $scope.MaxAmount = data.maxAmount;

                    // List country
                    $scope.ListCountry = data.listCountry;
                    $scope.CountrySelected = data.listCountry[0];

                    // List bank
                    $scope.ListBank = data.listBank;
                    $scope.BankSelected = data.listBank[0];
                } else { // edit
                    if (data.advertisementUser !== null || data.advertisementUser !== undefined) {
                        $scope.IsValidUsdRate = true;
                        $scope.Id = data.advertisementUser.id;

                        $scope.UsdRate = data.advertisementUser.bitUSDPrice;

                        $scope.ReferenceExchange = data.referenceExchange;
                        $scope.Fee = data.fee;

                        $scope.BitcoinPriceMaker = $scope.ReferenceExchange * $scope.UsdRate;
                        $scope.BitcoinPriceTaker = $scope.BitcoinPriceMaker - ($scope.BitcoinPriceMaker * $scope.Fee);

                        $scope.TextDetail = ($scope.IsDetail) ? "Cơ bản" : "Chi tiết";

                        // List currency
                        $scope.ListCurrency = data.listCurrency;
                        $scope.CurrencySelected = data.listCurrency.find(x => x.id === data.advertisementUser.currencyId);

                        // Min,max amount
                        $scope.MinAmount = data.advertisementUser.minAmount;
                        $scope.MaxAmount = data.advertisementUser.maxAmount;

                        // List country
                        $scope.ListCountry = data.listCountry;
                        $scope.CountrySelected = data.listCountry.find(x => x.id === data.advertisementUser.countryId);

                        // List bank
                        $scope.ListBank = data.listBank;
                        $scope.BankSelected = data.listBank.find(x => x.id === data.advertisementUser.bankId);

                        // Select box
                        $('#ReferenceExchange').val(data.advertisementUser.referenceExchange);
                        $('#PaymentMethod').val(data.advertisementUser.paymentMethod);
                        $('#PaymentTime').val(data.advertisementUser.paymentTime);
                    } else {
                        showErrorToastr('Error');
                    }
                }

                // apply
                $scope.$apply();
                Loading.Hide();
            }
        });
    }

    var id = $scope.Id;
    loadData(parseInt(id));
}]);