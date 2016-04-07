function Messsage(elementName, Titre, Message, Severity)
{
	var severity;

	switch (Severity) {
		case 1: //error
			severity='danger';
			break;
		case 2: //warning
			severity='warning';
			break;
		case 3://Info
			severity='info';
			break;
		case 4://success
			severity='success';
			break;

	}

	document.getElementById(elementName).innerHTML= 
								"<div class='bs-callout bs-callout-" + severity + " fade in'>" +
									"<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button>"+
									"<h4>"+ Titre + "</h4>"+
									"<p>" + Message + "</p>"+
								"</div>"
									
}
