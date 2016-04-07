var myMessages = ['info', 'warning', 'error', 'success']; // define the messages types		
var myFormName;
var myValidator;
var myRules;
var myRulesMessages;
var myApplicationName;
var myTheme;
var myControlerName;
var myControlerUrl;
var myControlerApplicationName;


function getMessageError() {
    
    document.write("<div class='info message'></div>");
    document.write("<div class='error message'></div>");
    document.write("<div class='warning message'></div>");
    document.write("<div class='success message'></div>");

}

function getControlerPath(functiontocall) {

    var result;
    if (myApplicationName != null && myControlerName != null && myControlerApplicationName != null) {
        result = myControlerUrl + '/' + myControlerApplicationName + '/' + myControlerName + '/' + functiontocall;
        //jQuery.support.cors = false; //true pour les appels dans un autre site.
    }
    else
    {
        result = getRootURL() + '/' + myApplicationName + '/' + myControlerName + '/' + functiontocall;
    }

    return result;
}

function getControlerPathCommand(controlerToCall, functiontocall) {

    var result;
    if (myApplicationName != null && controlerToCall != null && myControlerApplicationName != null) {
        if (myControlerApplicationName == "") {
            result = myControlerUrl + '/' + controlerToCall + '/' + functiontocall;
        }
        else {
            result = myControlerUrl + '/' + myControlerApplicationName + '/' + controlerToCall + '/' + functiontocall;
        }        
    }
    else {
        result = getRootURL() + '/' + myApplicationName + '/' + controlerToCall + '/' + functiontocall;
    }

    return result;
}

//Cache l'ensemble des messages
function hideAllMessages() {
    var messagesHeights = new Array(); // this array will store height for each

    for (i = 0; i < myMessages.length; i++) {
        messagesHeights[i] = $('.' + myMessages[i]).outerHeight();
        $('.' + myMessages[i]).css('top', -messagesHeights[i]); //move element outside viewport	  
    }
}

//Cache un message
function showMessageClick(type) {
    $('.' + type + '-trigger').click(function () {
        hideAllMessages();
        $('.' + type).animate({ top: "0" }, 500);
    });
}

function clearMessage(type) {
    $('.' + type).empty();
}

function clearAllMessage() {
    var messagesHeights = new Array();
    for (i = 0; i < myMessages.length; i++) {       
        messagesHeights[i] = $('.' + myMessages[i]).outerHeight();
        $('.' + myMessages[i]).css('top', -messagesHeights[i]); //move element outside viewport	
        $('.' + myMessages[i]).empty();
    }
}

//Affiche un message
function showMessage(titre,type, msg) {
    hideAllMessages();

    var content;
    contents = '<h3>' + titre + '</h3>';
    contents = contents + '<p>' + msg + '</p>';
    
    $('.' + type).append(contents);
    $('.' + type).animate({ top: "0" }, 500);
    $('.' + type).show();

}
 
//Retruove les parametre d'un URL
function getParameter(paramName) {
    var searchString = window.location.search.substring(1),
        i, val, params = searchString.split("&");

    for (i = 0; i < params.length; i++) {
        val = params[i].split("=");
        if (val[0] == paramName) {
            return unescape(val[1]);
        }
    }
    return null;
}

//permet de convertire une severité du framework makesens pour le UI.
function convertSeverity(typeInt) {
    
    return myMessages[typeInt];
}

//Traite le ViewLogics pour identifier les messages d'erreur
function analysViewLogics(resultdata) {
    ViewLogics = resultdata.ViewLogics;
    if (ViewLogics != null) {
        //affiche un message
        if (ViewLogics.ViewMessage != null) {
            if (ViewLogics.ViewMessage.Item2 != null) {
                //showMessage('info', data.d.ViewLogics.ViewMessage.Item1);
                var titre = ViewLogics.ViewMessage.Item1;
                var msg = ViewLogics.ViewMessage.Item2;
                var type = ViewLogics.ViewMessage.Item3;
               
                showMessage(titre,convertSeverity(type), msg);
            }
        }
        //validation des etats
        //Security
        if (ViewLogics.ViewDisplay != null) {
            if (ViewLogics.ViewDisplay != null) {

                if (myFormName != null) {
                    fillFormDisplay(ViewLogics.ViewDisplay);
                }
            }
        }

        //Security
        if (ViewLogics.ViewSecurityDisplay != null) {
            if (ViewLogics.ViewSecurityDisplay != null) {

                if (myFormName != null) {
                    fillFormSecurity(ViewLogics.ViewSecurityDisplay);
                }
            }
        }

        //systeme status
        if (ViewLogics.ViewSystemStatus != null) {
            if (ViewLogics.ViewSystemStatus != null) {

                if (myFormName != null) {
                    fillFormStatus(ViewLogics.ViewSystemStatus);
                }
            }
        }
        //redirige
        if (ViewLogics.ViewGolocalize != null) {
            if (ViewLogics.ViewGolocalize.Item1 != null) {
                $(location).attr('href', ViewLogics.ViewGolocalize.Item1);
            }
        }
        //message de validation d'affaire
        if (ViewLogics.ViewValidation != null) {
            if (ViewLogics.ViewValidation.Item1 != null) {
                //showMessage('info', data.d.ViewLogics.ViewMessage.Item1);

                for (var i in ViewLogics.ViewValidation.Item2) {
                    var titre = ViewLogics.ViewValidation.Item1;
                    var type = ViewLogics.ViewValidation.Item2[i].Severity
                    var msg = ViewLogics.ViewValidation.Item2[i].Description;
                    showMessage(titre,convertSeverity(type), msg);
                }

            }
        }
        //Message de reservation
        if (ViewLogics.ViewMessageReservation != null) {
            if (ViewLogics.ViewMessageReservation.Item1 != null) {
                //showMessage('info', data.d.ViewLogics.ViewMessage.Item1);
                var titre = ViewLogics.ViewMessage.Item1;
                var msg = ViewLogics.ViewMessage.Item2;
                var type = ViewLogics.ViewMessage.Item3;

                showMessage(titre,convertSeverity(type), msg);
            }
        }
        

        //Validation
        if (ViewLogics.ViewAssociateViewToValidation != null) {
            if (ViewLogics.ViewAssociateViewToValidation != null) {
                
                if (myFormName != null && ViewLogics.ViewAssociateViewToValidation.Item2 != null && ViewLogics.ViewAssociateViewToValidation.Item1!=null)
                {
                    //pour les valisations JQWidget
                    formJqValidation(myFormName, ViewLogics.ViewAssociateViewToValidation.Item1, ViewLogics.ViewAssociateViewToValidation.Item2, resultdata);
                }
            }
        }

    }
}

function fillFormStatus(data) {
    jQuery.each(data, function (item, dataitem) {
        var key = dataitem.Key;
        var value = dataitem.value;

        switch (key) {
            case 'fileversionframework':

                break;
            case 'applicationcode':

                break;
            case 'versionservice':

                break;
            case 'authenticateduser':

                break;
            case 'authenticationtype':

                break;
            case 'isauthenticated':

                break;
            case 'applicationuser':

                break;
            case 'applicationcodedescription':

                break;
            case 'environnement':

                break;
            default:

        }

    });
}

//Assigne l'etat des controles
function fillFormDisplay(data) {

    jQuery.each(data, function (item, itemdata) {
        if ($("#" + itemdata.Item3).exists()) {
            
            switch (itemdata.Item2) {
                case 0://Visible
                    if (itemdata.Item1 == true)
                        $("#" + itemdata.Item3).show();
                    else
                        $("#" + itemdata.Item3).hide();
                    break;
                case 1://Enabled
                    var isdisabled = false;
                    if (itemdata.Item1 == false)
                        isdisabled = true;

                    $("#" + itemdata.Item2).attr('disabled', isdisabled);
                    break;

            }


        }
    });

}

//Assigne la securite
function fillFormSecurity(data) {

    jQuery.each(data, function (item, itemdata) {
        if ($("#" + itemdata.Item2).exists()) {
            var isdisabled = false;
            if (itemdata.Item1 == false)
                isdisabled = true;

            $("#" + itemdata.Item2).attr('disabled', isdisabled);
            
        }
    });

}

//Remplie et affiche le formulaire
function fillForm(data, theme, gridname) {
    jQuery.each(data, function (item, itemdata) {



        if ($("#" + item).exists()) {

            if ($("#" + item).is('label')) {
                $("#" + item).text(itemdata);
            } else if ($("#" + item).is('input') && $("#" + item).attr('type') != 'button') {
                //$("#" + item).jqxInput({theme: theme });
                //$("#" + item).jqxInput('val',itemdata);
                $("#" + item).val(itemdata);
            } else if ($("#" + item).is('input') && $("#" + item).attr('type') == 'button') {
                $('#' + item).jqxButton({ theme: theme });
                $("#" + item).val(itemdata);
            } else if ($("#" + item).is('checkbox')) {
                $('#' + item).jqxCheckBox({ theme: theme });
                $("#" + item).val(itemdata);
            } else if ($("#" + item).is('hidden')) {
                $("#" + item).val(itemdata);
            } else if (jQuery.isFunction($('#' + item).jqxDateTimeInput) && itemdata.indexOf("Date") > 0) {
                var value = new Date(parseInt(itemdata.substr(6)));
                if (value != 'NaN') {

                    $('#' + item).jqxDateTimeInput({ theme: theme, formatString: "dd/MM/yyyy", value: $.jqx._jqxDateTimeInput.getDateTime(value) });

                }

            } else {

                $("#" + item).val(itemdata);
            }

        }
        else {
            if (gridname != null) {
                if ($("#" + gridname).exists() && item!='__type' && item!='PageParameters' && item!='ViewLogics' ) {

                    $("#" + gridname).jqxGrid('setcolumnproperty', item, 'text', itemdata);

                }
            }
        }

    });
  
}

function JSONDate(dateStr) {
    var m, day;
    jsonDate = dateStr;
    var d = new Date(parseInt(jsonDate.substr(6)));
    m = d.getMonth() + 1;
    if (m < 10)
        m = '0' + m
    if (d.getDate() < 10)
        day = '0' + d.getDate()
    else
        day = d.getDate();
    return (m + '/' + day + '/' + d.getFullYear())
}

function JSONDateAllreadyFormated(dateStr) {
    var m, day;
    jsonDate = dateStr;
    var d = new Date(dateStr);
    m = d.getMonth() + 1;
    if (m < 10)
        m = '0' + m
    if (d.getDate() < 10)
        day = '0' + d.getDate()
    else
        day = d.getDate();
    return (m + '/' + day + '/' + d.getFullYear())
}

function JSONDateWithTime(dateStr) {
    jsonDate = dateStr;
    var d = new Date(parseInt(jsonDate.substr(6)));
    var m, day;
    m = d.getMonth() + 1;
    if (m < 10)
        m = '0' + m
    if (d.getDate() < 10)
        day = '0' + d.getDate()
    else
        day = d.getDate();
    var formattedDate = m + "/" + day + "/" + d.getFullYear();
    var hours = (d.getHours() < 10) ? "0" + d.getHours() : d.getHours();
    var minutes = (d.getMinutes() < 10) ? "0" + d.getMinutes() : d.getMinutes();
    var formattedTime = hours + ":" + minutes + ":" + d.getSeconds();
    formattedDate = formattedDate + " " + formattedTime;
    return formattedDate;
}

Date.prototype.toMSJSON = function () {
    var date = '\\\/Date(' + this.getTime() + ')\\\/';
    return date;
};

function getForm(formname) {
    return getFormCommand(formname, null);
}

function getFormCommand(formname, command) {

    var obj = new Object();

    var identity = "#" + formname + " input";

    $(identity).each(function () {

        if ($(this).val() != "Submit" && $(this).attr('type')!="button") {
            var propertyName = $(this).attr('Name');
            var propertyValue;
            
            if (jQuery.isFunction($('#' + propertyName).jqxDateTimeInput) ) {
                propertyValue = $('#' + propertyName).jqxDateTimeInput('getDate');
            }
            
            if (typeof propertyValue == "undefined" || propertyValue==null) {
                propertyValue = $(this).val();
            }
            else {
            
                var dt1 = new Date(Date.UTC(propertyValue.getFullYear(), propertyValue.getMonth(), propertyValue.getDate(), propertyValue.getHours(), propertyValue.getMinutes(), propertyValue.getSeconds(), propertyValue.getMilliseconds()));
                var propertyValue = dt1.toMSJSON();
                
            }
           
            eval("obj." + propertyName + '="' + propertyValue + '"');
        }
    });

    if (command != null)
        eval("obj.commandToExecute=" + command + '"');//pour le passage des commande au JSON
    
    return JSON.stringify(obj);
}

//Fait l'association des messages de validation au controle ui de la page. Jquery de base seulement
function formValidation(frmName,rule,message) {

    var object = eval("var objRule=" + rule )
    var object = eval("var objMessage=" + message)

    myRules = objRule;
    myRulesMessages = objMessage;
    
    //form validation rules
    $("#" + frmName).validate({
        rules: myRules,
        messages: myRulesMessages,
        submitHandler: function (form) {
            form.submit();
        }
    });   
   

}

function getAbsolutePath() {
    var loc = window.location;
    var pathName = loc.pathname.substring(0, loc.pathname.lastIndexOf('/') + 1);
    return loc.href.substring(0, loc.href.length - ((loc.pathname + loc.search + loc.hash).length - pathName.length));
}

function getRootURL() {
    var baseURL = location.href;
    var rootURL = baseURL.substring(0, baseURL.indexOf('/', 7));

    // if the root url is localhost, don't add the directory as cassani doesn't use it
    if (baseURL.indexOf('localhost') == -1) {
        return rootURL + "/AppName/";
    } else {
        return rootURL + "/";
    }
}
//verifi si le control existe
jQuery.fn.exists = function () { return jQuery(this).length > 0; }

//Fait l'association des messages de validation au controle ui de la page. Jquery de base seulement
function formJqValidation(frmName, rule, message,receivedata,theme) {

    var object = eval("var objRule=" +  rule)
    var object = eval("var objMessage=" + message)

    myRules = objRule;
    myRulesMessages = objMessage;

  
    var privaterule
    var valrules;


    jQuery.each(myRules, function (uicontrol, itemrules) {
        
        if ($("#" + uicontrol).exists()) {
            jQuery.each(itemrules, function (ruletype, parameter) {
                if (ruletype) {
                }
                jQuery.each(myRulesMessages[uicontrol], function (msgruletype, msg) {
                    if (msgruletype == ruletype) {

                        switch (ruletype) {
                            case "DateGreaterOrEqualThan":
                                //cote serveur
                                break;
                            case "DateFormat":
                                //non applicable
                                break;
                            case "DateGreaterThan":
                                var strfunc = "function (input, commit) {var date = $('#" + uicontrol + "').jqxDateTimeInput('value');var result = date.dateTime.getDate() >  Date(parseInt(" + parameter + ".substr(6)));return result;}";
                                privaterule = "{input: '#" + uicontrol + "', message: \"" + msg + "\", action: 'valuechanged', rule: " + strfunc + "}";

                                break;
                            case "DateGreaterOrEqualThan":
                                //cote serveur
                                break;
                            case "DateLessOrEqualThan":
                                //cote serveur
                                break;
                            case "DateLessThan":
                                //cote serveur
                                break;
                            case "DateRequired":
                                
                                break;

                            case "DateCustomRules":
                                var controler = getControlerPath('DateCustomRules');

                                var callback = " var result = getFunctionPOST('" + controler + "', \"\{ 'ObjectReference':'" + receivedata.__type + "','PropertyReference':'" + uicontrol + "','parameters':'\"+ data+ \"' \}\",false);";
                                var comparaison = "commit(result);  return result;";

                                var data = " JSONDateAllreadyFormated($('#" + uicontrol + "').jqxDateTimeInput('getDate'));";
                                var strfunc = "function (input, commit) {var data = " + data + callback + comparaison + "}";
                                privaterule = "{input: '#" + uicontrol + "', message: \"" + msg + "\", action: 'valuechanged', rule: " + strfunc + "}";

                                break;
                            case "CustomRules":
                                var controler = getControlerPath('DateCustomRules');

                                var callback = " var result = getFunctionPOST('" + controler + "', \"\{ 'ObjectReference':'" + receivedata.__type + "','PropertyReference':'" + uicontrol + "','parameters':'\"+ data+ \"' \}\",false);";
                                var comparaison = "if(result==true) {commit (true);} else {commit(false);}  return result;";

                                var data = "$('#" + uicontrol + "').val();";
                                var strfunc = "function (input, commit) {var data = " + data + callback + comparaison + "}";
                                privaterule = "{input: '#" + uicontrol + "', message: \"" + msg + "\", action: 'valuechanged', rule: " + strfunc + "}";

                                break;
                            case "AlphanumericCharacters":
                                var regex = '([^A-Za-z0-9\-])';
                                var func = "$('#" + uicontrol + "').filter(function () {return this.id.match('" + regex + "');});";
                                var data = "$('#" + uicontrol + "').jqxDateTimeInput('getDate');";
                                var strfunc = "function (input, commit) {" + func + "}";
                                privaterule = "{input: '#" + uicontrol + "', message: \"" + msg + "\", action: 'valuechanged', rule: " + strfunc + "}";
                                break;
                            case "FloatingNumber":
                                
                                var regex = '^[+-]?\d+(\.\d+)?$';
                                var func = "$('#" + uicontrol + "').filter(function () {return this.id.match('" + regex + "');});";
                                var data = "$('#" + uicontrol + "').jqxDateTimeInput('getDate');";
                                var strfunc = "function (input, commit) {" + func + "}";
                                privaterule = "{input: '#" + uicontrol + "', message: \"" + msg + "\", action: 'valuechanged', rule: " + strfunc + "}";
                                break;
                            case "ListNotEmpty":
                                //cote serveur
                                break;
                            case "Luhn":
                                //cote serveur
                                break;
                            case "ObjectContainsOneValidProperty":
                                //non applicable
                                break;
                            case "ObjectRequired":
                                //non applicable
                                break;
                            case "RulePhoneNumberWithExtention":                                
                                var regex = '^(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$';
                                var func = "$('#" + uicontrol + "').filter(function () {return this.id.match('" + regex + "');});";
                                var data = "$('#" + uicontrol + "').jqxDateTimeInput('getDate');";
                                var strfunc = "function (input, commit) {" + func + "}";
                                privaterule = "{input: '#" + uicontrol + "', message: \"" + msg + "\", action: 'valuechanged', rule: " + strfunc + "}";
                                break;
                            case "RulePostalCodeCAN":
                                var regex = '^[ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ]( )?\d[ABCEGHJKLMNPRSTVWXYZ]\d$';
                                var func = "$('#" + uicontrol + "').filter(function () {return this.id.match('" + regex + "');});";
                                var data = "$('#" + uicontrol + "').jqxDateTimeInput('getDate');";
                                var strfunc = "function (input, commit) {" + func + "}";
                                privaterule = "{input: '#" + uicontrol + "', message: \"" + msg + "\", action: 'valuechanged', rule: " + strfunc + "}";
                                break;
                            case "RulePostalCodeUS":
                                privaterule = "{input: '#" + uicontrol + "', message: \"" + msg + "\", action: 'keyup', rule: 'zipCode'}";
                                break;
                            case "EmailAddress":
                                privaterule = "{input: '#" + uicontrol + "', message: \"" + msg + "\", action: 'keyup', rule: 'email'}";
                                break;
                            case "RegularExpression":
                                var func = "$('#" + uicontrol + "').filter(function () {return this.id.match('" + parameter + "');});";
                                var data = "$('#" + uicontrol + "').jqxDateTimeInput('getDate');";
                                var strfunc = "function (input, commit) {" + func + "}";
                                privaterule = "{input: '#" + uicontrol + "', message: \"" + msg + "\", action: 'valuechanged', rule: " + strfunc + "}";
                                break;
                            case "StringRequired":
                                privaterule = "{input: '#" + uicontrol + "', message: \"" + msg + "\", action: 'keyup', rule: 'required'}";
                                break;
                            case "PostalCode":
                                var regex = '(^[ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ]( )?\d[ABCEGHJKLMNPRSTVWXYZ]\d$)|(^\d{5}(-\d{4})?$)';
                                var func = "$('#" + uicontrol + "').filter(function () {return this.id.match('" + regex + "');});";
                                var data = "$('#" + uicontrol + "').jqxDateTimeInput('getDate');";
                                var strfunc = "function (input, commit) {" + func + "}";
                                privaterule = "{input: '#" + uicontrol + "', message: \"" + msg + "\", action: 'valuechanged', rule: " + strfunc + "}";
                                break;
                            case "PhoneNumber":
                                privaterule = "{input: '#" + uicontrol + "', message: \"" + msg + "\", action: 'keyup', rule: 'phone'}";
                                break;
                            case "IntegerMaxValue":
                                privaterule = "{input: '#" + uicontrol + "', message: \"" + msg + "\", action: 'keyup', rule: 'maxLength=" + parameter + "'}";
                                break;
                            case "StringMaxLength":
                                privaterule = "{input: '#" + uicontrol + "', message: \"" + msg + "\", action: 'keyup', rule: 'maxLength=" + parameter + "'}";
                                break;
                            case "IntergerMinValue":
                                privaterule = "{input: '#" + uicontrol + "', message: \"" + msg + "\", action: 'keyup', rule: 'minLength=" + parameter + "'}";
                                break;
                            case "StringMinLength":
                                privaterule = "{input: '#" + uicontrol + "', message: \"" + msg + "\", action: 'keyup', rule: 'minLength=" + parameter + "'}";
                                break;
                        }

                        //if (parameter != null && parameter != "") {
                        //    privaterule = "{input: '#" + uicontrol + "', message: \"" + msg + "\", action: 'keyup', rule: '" + ruletype + "=" + parameter + "'}";
                        //}
                        //else {
                        //    privaterule = "{input: '#" + uicontrol + "', message: \"" + msg + "\", action: 'keyup', rule: '" + ruletype + "'}";
                        //}
                    }
                });
                if (valrules != null)
                    valrules = valrules + "," + privaterule;
                else
                    valrules = privaterule;

            });

        }
        else {
            //showMessage('Attention erreur de conception','warning', "le control:" + uicontrol + " n'a pas été trouvé dans le formulaire. pour assigner la validation");
            //TODO: il faut loger si pas trouvg de control
        }
    });

  
    var object = eval("var objRule=[" + valrules + "]")

    myRules = objRule;
    // initialize validator.    
    $("#" + myFormName).jqxValidator({ rules: myRules, theme: myTheme });
   
}

function getFunctionPOST(pUrl, parameters, asyn) {
    getFunctionPOST(pUrl, parameters, asyn, false)
}

//Appel une fonction d'affaire
function getFunctionPOST(pUrl,parameters,asyn,fillfrm) {
    var retrundata;

    // Create jqxProgressBar.            
   // $("#jqxprogressbar").jqxProgressBar({ animationDuration: 300, theme: myTheme, width: 250, height: 25,value:20 });
    
    jQuery.ajax({
        type: "POST",
        url: pUrl,
        //data: JSON.stringify({ id: row }),
        data: parameters,
        dataType: "json",
        async: asyn,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            retrundata = data.d;
          

            analysViewLogics(retrundata);
            
            //si en mode asyncrone on remplie le formulaire
            if (fillfrm == true) {
                //$("#jqxprogressbar").jqxProgressBar({ value: 100 });
                //Affiche l'information dans le formulaire
                fillForm(retrundata, myTheme);
            }
           
            //$('#jqxprogressbar').jqxProgressBar('destroy');

        },
        error: function (data) {
            //$('#jqxprogressbar').jqxProgressBar('destroy');
            showMessage('Erreur technique', 'error', data.responseText);
            //alert(data.responseText);
        }
        
    });

    

    return retrundata;

}

function getFunctionCommand(controlerName, parameters, functionToCall, asyn, fillfrm) {
    getFunctionCommand(controlerName, parameters, functionToCall, asyn, fillfrm, 'json');
}

function getFunctionCommand(controlerName, parameters, functionToCall, asyn, fillfrm, json) {
    
    var commandName = 'ExecuteCommandParameters';
    var retrundata;

    var controler = getControlerPathCommand(controlerName, commandName);
    var parameter = JSON.stringify({ parameters: parameters, command: functionToCall });

    jQuery.ajax({
        type: "POST",
        url: controler,
        //data: JSON.stringify({ id: row }),
        data: parameter,
        dataType: json,
        async: asyn,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            retrundata = data.d;


            analysViewLogics(retrundata);

            //si en mode asyncrone on remplie le formulaire
            if (fillfrm == true) {
                //Affiche l'information dans le formulaire
                fillForm(retrundata, myTheme);
            }

        },
        error: function (data) {

            showMessage('Erreur technique', 'error', data.responseText);

        }

    });

    function CallPageMethod(methodName, onSuccess, onFail) {
        var args = '';
        var l = arguments.length;
        if (l > 3) {
            for (var i = 3; i < l - 1; i += 2) {
                if (args.length != 0) args += ',';
                args += '"' + arguments[i] + '":"' + arguments[i + 1] + '"';
            }
        }
        var loc = window.location.href;
        loc = (loc.substr(loc.length - 1, 1) == "/") ? loc + "default.aspx" : loc;
        $.ajax({
            type: "POST",
            url: loc + "/" + methodName,
            data: "{" + args + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: onSuccess,
            fail: onFail
        });
    }

    return retrundata;

}

function getFunctionCommandGRID(controlerName, functionToCall, asyn, fillfrm ) {

    var commandName = 'ExecuteCommandGrid';
    var retrundata;

    var controler = getControlerPathCommand(controlerName, commandName);
    //var parameter = JSON.stringify({ parameters: parameters, command: functionToCall});

    jQuery.ajax({
        type: "GET",
        url: controler,
        cache:false,
        dataType: 'JSON',
        async: asyn,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            retrundata = data.d;
            analysViewLogics(retrundata);

            //si en mode asyncrone on remplie le formulaire
            if (fillfrm == true) {
                //Affiche l'information dans le formulaire
                fillForm(retrundata, myTheme);
            }

            },
            error: function (data) {

            showMessage('Erreur technique', 'error', data.responseText);

        }

    });



return retrundata;

}

//Appel une fonction d'affaire
function getFunctionCommandPOSTCmdName(controlerName,commandName,parameters,functionToCall,asyn,fillfrm) {
    var retrundata;

    var controler = getControlerPathCommand(controlerName, commandName);
    var parameter = JSON.stringify({ JSONParameter: parameters, command: functionToCall });

    jQuery.ajax({
        type: "POST",
        url: controler,
        //data: JSON.stringify({ id: row }),
        data: parameter,
        dataType: "json",
        async: asyn,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            retrundata = data.d;
          

            analysViewLogics(retrundata);
            
            //si en mode asyncrone on remplie le formulaire
            if (fillfrm == true) {
                //Affiche l'information dans le formulaire
                fillForm(retrundata, myTheme);
            }

        },
        error: function (data) {

            showMessage('Erreur technique', 'error', data.responseText);

        }
        
    });

    

    return retrundata;

}

//Appel une fonction d'affaire
function getFunctionGET(pUrl, parameters, asyn) {
    var retrundata;
    jQuery.ajax({
        type: 'GET',
        url: pUrl,
        //data: JSON.stringify({ id: row }),
        data: parameters,
        dataType: "json",
        async: asyn,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            retrundata = data.d;

            analysViewLogics(retrundata);

        },
        error: function (data) {
            showMessage('Erreur technique', 'error', data.responseText);
            //alert(data.responseText);
        }

    });
    return retrundata;

}


//Creation d'un adapter qui appel la logique d'affaire
function getDataAdapterCommand(fields, idItem, controlerName, functionToCall, gridname, parameters) {
    var retrundata;
    var commandName = 'ExecuteCommandParameters';

    var controler = getControlerPathCommand(controlerName, commandName);
    var parameter = JSON.stringify({ JSONParameter: parameters, command: functionToCall });

    //var paramerter = "{ 'command': '" + functionToCall + "'}";
    var source = {
        type: "POST",
        datatype: "json",
        datafields:fields,
        id: idItem,
        url: controler,        
        cache: false,
        formatdata: function (data) {
            return parameter;
        }
    };

    
    //Preparing the data for use
    var dataAdapter = new $.jqx.dataAdapter(source, {
        contentType: 'application/json; charset=utf-8',
        data: parameter,
        downloadComplete: function (data, textStatus, jqXHR) {
            retrundata = data.d;

            return data.d;
        },
        loadComplete: function (data) {
            fillForm(data, myTheme, gridname);
            analysViewLogics(data);
        },
        loadError: function (xhr, status, error) {
            showMessage('Erreur technique', status, xhr.responseText);
        }
    }

    );

    return dataAdapter;
}

function getDataAdapter(source, gridname) {
    var retrundata;
    //Preparing the data for use
    var dataAdapter = new $.jqx.dataAdapter(source, {
        contentType: 'application/json; charset=utf-8',
        downloadComplete: function (data, textStatus, jqXHR) {
            retrundata = data.d;

            return data.d;
        },
        loadComplete: function (data) {
            fillForm(data, myTheme, gridname);
            analysViewLogics(data);
        },
        loadError: function (xhr, status, error) {
            showMessage('Erreur technique', 'error', xhr.responseText);
        }
    }

    );

    return dataAdapter;
}

function ExecuteAction(formname, functionname, buttonname, asyc) {
    // validate form.
    var ret=false;
    $("#" + buttonname).click(function () {
        clearAllMessage();

        var controler = getControlerPath(functionname);

        var parameter = getJSONParamter(formname);

        data = getFunctionPOST(controler, parameter, asyc);
      
        ret=true;
    });
    return ret;

}

function ExecuteActionForm(controlerName,formname,functionname, buttonname, asyc) {
    // validate form.
    var ret = false;
    $("#" + buttonname).click(function () {
        clearAllMessage();

        var parameter =  getForm(formname);

        data = getFunctionCommandPOSTCmdName(controlerName, 'ExecuteCommandForm', parameter, functionname, asyc, false);

        ret = true;
    });
    return ret;

}
function ExecuteActionFormView(controlerName, formname, functionname, buttonname, asyc) {
    // validate form.
    var ret = false;
    $("#" + buttonname).click(function () {
        clearAllMessage();

        var parameter = getForm(formname);

        data = getFunctionCommandPOSTCmdName(controlerName, 'ExecuteCommandFormView', parameter, functionname, asyc, false);

        ret = true;
    });
    return ret;

}


function getJSONParamter(FormName) {
    return JSON.stringify({ JSONParameter: getForm(FormName) });
}

function ValidateForm(controlerName,formname, functionname, buttonSavename, isFillForm, theme) {

    var ret = false;
    
    // validate form.
    $("#" + buttonSavename).click(function () {
        var validationResult = function (isValid) {
            if (isValid) {
                //$('#' + myFormName).submit();
                $('#' + myFormName).fadeIn('fast');
            }
        }
        $('#' + formname).jqxValidator('validate', validationResult);
    });
    $('#' + formname).on('validationSuccess', function (event) {
        clearAllMessage();
        var controlerSaveEmployer = getControlerPath(functionname);

        //var parameter = getJSONParamter(formname);

        //data = getFunctionPOST(controlerSaveEmployer, parameter, false);
        var parameter = getForm(formname);

        data = getFunctionCommandPOSTCmdName(controlerName, 'ExecuteCommandPOST', parameter, functionname, false, false);
        analysViewLogics(data);
        if(isFillForm==true)
        {
            fillForm(data, theme);
        }
    
    ret = true;
    });

    $('#' + formname).on('validationError', function (event) {
        //TODO:
        ret = false;
    });

    return ret;
}

function getTheme(ptheme) {
   
    var url =getRootURL() + myApplicationName + "/jqwidgets/styles/jqx." + ptheme + '.css';

    //if (document.createStyleSheet != undefined) {
    //    var hasStyle = false;
    //    $.each(document.styleSheets, function (index, value) {
    //        if (value.href != undefined && value.href.indexOf(ptheme) != -1) {
    //            hasStyle = true;
    //            return false;
    //        }
    //    });
    //    if (!hasStyle) {
            document.createStyleSheet(url);
        //}
    //}
    //else $(document).find('head').append('<link rel="stylesheet" href="' + url + '" media="screen" />');

    return ptheme;
};




$("body").on({
    // When ajaxStart is fired, add 'loading' to body class
    ajaxStart: function () {
        //$(this).addClass("loading");
        //$("#loading").jqxWindow({ isModal: true, height: 90, width: 150, theme: myTheme, position: 'center', animationType: 'combined', showAnimationDuration: 200,theme:myTheme });
        //$('#loading').jqxWindow('open');

        //$("#spinner").show();
    },
    // When ajaxStop is fired, rmeove 'loading' from body class
    ajaxStop: function () {
        //$(this).removeClass("loading");
        //$("#spinner").hide();
        //$('#loading').jqxWindow('close');
    }
});

$(document).ready(function () {

    // Initially, hide them all
    hideAllMessages();

    // When message is clicked, hide it
    $('.message').click(function () {
        $(this).animate({ top: -$(this).outerHeight() }, 300);
    });

});