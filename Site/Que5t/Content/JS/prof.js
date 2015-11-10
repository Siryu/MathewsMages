/*jslint vars: true, plusplus: true, devel: true, nomen: true, indent: 4, maxerr: 50 */
/*global define */
/*jslint browser: true*/
/*global $, jQuery, alert*/

$(document).ready(function () {
    "use strict";

    $('#stats').hide();
    $('#rooms').hide();
    $('#kills').hide();

    $('#swerd').on('click', function () {
        $('#stats').fadeOut('fast');
        $('#rooms').fadeOut('fast');
        $('#kills').fadeIn(1000);
    });

    $('#swerd').on('dblclick', function () {
        $('#kills').fadeOut('fast');
    });

    $('#merp').on('click', function () {
        $('#stats').fadeOut('fast');
        $('#kills').fadeOut('fast');
        $('#rooms').fadeIn(1000);
    });

    $('#merp').on('dblclick', function () {
        $('#rooms').fadeOut('fast');
    });

    $('#sherld').on('click', function () {
        $('#rooms').fadeOut('fast');
        $('#kills').fadeOut('fast');
        $('#stats').fadeIn(1000);
    });

    $('#sherld').on('dblclick', function () {
        $('#stats').fadeOut('fast');
    });


});