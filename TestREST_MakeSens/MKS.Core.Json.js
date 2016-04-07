/*
Effectue la communication en REST avec la couche de présentation

Fichier : MKS.Core.JSon
Version : 3.0.0.13
Compagnie : GBSens inc.
Produit : MakeSens
Autheur : Stéphane Dufour

*/



FormGroup = function (formName) {
    
    this.Name = formName;
    this.ControlMessage;
    this.CallBackMessageFonction = function (callBackFunctionMessagte) {
        MessageFunction = $.CallBacks();
        MessageFunction.add(callBackFunctionMessagte);
    };
    this.MessageFunction;
    this.Validators;
    this.ShowMessage = function(title, message, severity)
    {

        if (this.ControlMessage != null && this.CallBackMessageFonction=='function') {
            //Message(this.ControlMessage, title, message, severity);
            MessageFunction.fire(this.ControlMessage, title, message, severity);
        }
        else
        {
            alert(message);
        }
    }
}

Controler = function (url, controlerName,isAsync,attachedform) {
 
    //Fomulaire attaché au controleur pour permettre la lecture/ecriture automatique
    this.AttachedForm = attachedform;
    //Détermine si on attend la réponse du controleur ou non
    this.isAsynchrone = isAsync;
    // Il faut toujours un GET car le Json contient possiblement de l'information
    this.Type = 'GET';
    this.URL = null;
    this.DataType = 'json';
    //Parametre à passer au controler.
    this.Parameters = null;
    this.ContentType = 'application/json; charset=utf-8';

    //Envoi une commande au controler
    this.SendCommand = function (functionName, parameter)
    {
        if(attachedform!=null)
        {
            return ExecuteCommandFillForm(functionName, parameter);
        }
        else
        {
            return ExecuteCommand(functionName, parameter);
        }
    }

    //Appelé si aucun formulaire est passé au controler ajax
    ExecuteCommand = function (functionName, parameter) {
   
    if (parameter == null) {
        //parameters = JSON.stringify({ command: functionName });
        this.Parameters = { 'command': JSON.stringify(functionName) }
        this.URL = url + '/' + controlerName + '/' + 'ExecuteCommand'; 
    }
    else
    {
        this.Parameters = { 'parameters': JSON.stringify(parameter), 'command': JSON.stringify(functionName) };
        this.URL = url + '/' + controlerName + '/' + 'ExecuteCommandParameters';
    }
    
    var data = ExecuteAjax(this);
    return data;

    }

    //Appelé si un formulaire est passé au controleur ajax
    ExecuteCommandFillForm = function (functionName, parameter) {

    if (parameter == null) {
        //parameters = JSON.stringify({ command: functionName });
        this.Parameters = { 'command': JSON.stringify(functionName) }
        this.URL = url + '/' + controlerName + '/' + 'ExecuteCommand';
    }
    else {
        this.Parameters = { 'parameters': JSON.stringify(parameter), 'command': JSON.stringify(functionName) };
        this.URL = url + '/' + controlerName + '/' + 'ExecuteCommandParameters';
    }

    var data = ExecuteAjax(this);
    if (this.AttachedForm != null)
        this.FillForm(data,this.AttachedForm);

    return data;
}  

    //Avant d'envoyer la commande, la fonction effectue la lecture du formulaire
    this.SendCommandReadForm = function (functionName, refillForm) {
      
        if (this.AttachedForm != null) {
        //this.Parameters = { 'JSONView': ReadForm(formName), 'command': JSON.stringify(functionName) }      
            this.Parameters = { 'parameters': this.ReadForm(), 'command': JSON.stringify(functionName) }
        this.URL = url + '/' + controlerName + '/' + 'ExecuteCommandFormView';
    
        var data = ExecuteAjax(this);
        if (refillForm != null && refillForm==true)
            {
            this.FillForm(data);
            }
        }        

    return data;
}

    //Creer la vue en fonction du formulaire
    //Le nom des inputs doivent correspondres au nom des propiété de la vue.
    this.ReadForm = function () {
        
       
        var elements = document.forms[this.AttachedForm.Name].elements;

        var obj = new Object();

        for (var i = 0; i < elements.length; i++) {
            var property = elements[i].name;

            if (property != '') {
                if (isDate(elements[i].value)) {
                    var valuedate = new Date(elements[i].value)
                    eval("obj." + property + '="' + valuedate.toUTCString() + '"');
                }
                else {
                    var value = elements[i].value;
                    eval("obj." + property + '="' + value + '"');
                }
            }

        }

        return JSON.stringify(obj);
    }

    //Rempli le formulaire automatiquement
    this.FillForm = function (data, formGroup) {
        
        

        if (data != null && formGroup != null) {

            $.each(data, function (item, itemdata) {



                var elements = document.forms[formGroup.Name].elements;


                var obj;
                if (item != null && item != '__type') {
                    if (itemdata != null)
                        obj = typeof itemdata;


                    if (obj != null && elements[item] != null) {


                        //if (elements[item].localName == 'input' ) {
                        if (obj == 'string' && itemdata != null && itemdata.indexOf("Date") > 0)
                            obj = 'date';

                        switch (obj) {
                            case 'boolean':
                                if (elements[item].type == 'checkbox' || elements[item].type == 'option')
                                    elements[item].checked = itemdata;
                                else
                                    elements[item].value = itemdata;
                                break;
                            case 'number':
                                elements[item].value = itemdata;
                                break;
                            case 'string':
                                elements[item].value = itemdata;

                                break;
                            case 'date':
                                //var value = new Date(parseInt(itemdata.substr(6)));
                                var date = eval(itemdata.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"));
                                elements[item].value = date.toString();
                                //$('#' + item).datepicker("setValue", date.toString());
                                break;
                            default:
                                elements[item].value = itemdata
                                break;
                        }

                        if (formGroup != null)
                            AssignControlViewForm(elements[item], data, formGroup.Validators);
                        else
                            AssignControlViewForm(elements[item], data);

                        //}
                    }
                }


            });
        }
        else
        {
            alert("Formulaire ou donnée non valide");
        }
    }

}

//Fonction pour la communication avec le controler REST
function ExecuteAjax(controler)
{
    var returndata;
    jQuery.ajax({
        type: controler.Type,
        url: controler.URL,
        data: controler.Parameters,
        dataType: controler.DataType,
        async: controler.isAsynchrone,
        contentType: controler.ContentType,
        success: function (data) {
            returndata = data.d;
            AnalyseView(returndata, controler);
        },
        error: function (data) {
            if (controler.AttachedForm != null)
                controler.AttachedForm.ShowMessage('JS:500, Erreur de communication', data.responseText, 'error');
            else
                alert(data.responseText);
        }

    });
    return returndata;
}



function AssignControlViewForm(element,data,validator)
{
    if (data.ViewLogics != null && data.ViewLogics.Validations != null && data.ViewLogics.Validations.UI != null) {
        for (var i = 0; i < data.ViewLogics.Validations.UI.length; i++) {
            var control = data.ViewLogics.Validations.UI[i];
            if (control.ObjectPropertyName == element.name) {
                //Assignation des validations
                if (validator != null)
                {
                    //Assigner la validation au control de validation
                }
                //Assignation des etats 
                if(control.ControlEnabled!=null)
                {
                    element.disabled = !control.ControlEnabled;
                }
                //if(control.ControlVisibled!=null)
                //    if (itemdata.Visible == false)
                //        elements[item].style.display = '';
                //    //var visible = control.ControlEnabled.PropertyDisplay
                //}
            }
        }
    }
    //element.setAttribute('required');
    //element.setAttribute('placeholder',data.ViewLogics.Validat);

    //data.ViewLogics.SecurityForms     (Item1, Item2)
    //data.ViewLogics.SecurityForms     (Item1, Item2)
    //data.ViewLogics.ControlEnabled
}

//Analyser le contenu du modèle reçu par la couche de présentation et agit en fonction du résultat
function AnalyseView(data, controler)
{
  
    if (data.ViewLogics.Messages != null)
    {
        if (controler.AttachedForm != null)
            controler.AttachedForm.ShowMessage(data.ViewLogics.Messages.Item1, data.ViewLogics.Messages.Item2, data.ViewLogics.Messages.Item3)
    }
    if (data.ViewLogics.SecurityMessages != null)
    {
        //TODO:loop into message
    }
    if (data.ViewLogics.ReservationMessages!=null)
    {
        //TODO:loop into message
    }
    if (data.ViewLogics.ContextValidationMessage != null)
    {
        //TODO:loop into message
    }
    if(data.ViewLogics.BusinessMessages!=null)
    {
        //TODO:loop into message
    }

  
}


// Fonction utilitaires
function isDate(val) {
    var d = new Date(val);
    return !isNaN(d.valueOf());
}
function convertToJSONDate(dt) {
    var newDate = new Date(Date.UTC(dt.getFullYear(), dt.getMonth(), dt.getDate(), dt.getHours(), dt.getMinutes(), dt.getSeconds(), dt.getMilliseconds()));
    return '/Date(' + newDate.getTime() + ')/';
}
function loadScript(url, callback) {

    var script = document.createElement("script")
    script.type = "text/javascript";

    if (script.readyState) {  //IE
        script.onreadystatechange = function () {
            if (script.readyState == "loaded" ||
                    script.readyState == "complete") {
                script.onreadystatechange = null;
                callback();
            }
        };
    } else {  //Others
        script.onload = function () {
            callback();
        };
    }

    script.src = url;
    document.getElementsByTagName("head")[0].appendChild(script);
}