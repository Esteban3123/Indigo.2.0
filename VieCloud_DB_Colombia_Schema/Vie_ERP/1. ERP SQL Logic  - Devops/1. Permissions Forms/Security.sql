/*==============================================================================================================================
	Author: Ariadna Sophia Cabrera Carrera                                                                        
	PBI : #23092
	Sprint : ERP_Services\HCM_week_6-8 (2025)
==============================================================================================================================*/
INSERT INTO Security.FormAction (IdForm, IdAction)
	SELECT f.Id, a.Id
	FROM Security.Form f
	CROSS JOIN Security.Action a
	WHERE f.Name = 'Talento Humano'
	AND (a.Name = 'Visualizar Reporte' OR a.Name = 'Imprimir Reporte')

	/*==============================================================================================================================
Author: Ariadna Sophia Cabrera Carrera 
	PBI : 23859
	Sprint : ERP_Services\SCM_week_14-16 (2025)
==============================================================================================================================*/

DECLARE @IdForm INT 
SET @IdForm = (SELECT ISNULL(MAX(Id), 0) + 1 FROM Security.Form)

INSERT INTO Security.Form (Id, Name, PrintEvents, HasSequence, IsNativeForm,
		HasForm, ClassName, AssemblyName, HandlesMassiveConfirm, SequenceModule, State) 
	VALUES (@IdForm, 'Informe de Renta', 'SUCA', 0, 1, 1, 
		'FrmReportIncomeTax', 'Presentation.Payroll', 0, 'Payroll', 1)

INSERT INTO Security.ModuleForm (IdModule, IdTitle, IdForm, FormOrder)
	VALUES(53, 4, @IdForm, 1)

	DECLARE @NewFormId INT
SET @NewFormId = (SELECT Id FROM Security.Form WHERE Name = 'Informe de Renta');

INSERT INTO Security.FormAction (IdForm, IdAction)
SELECT 
    @NewFormId AS IdForm,
    IdAction
FROM Security.FormAction
WHERE IdForm = 2193;

/*==============================================================================================================================
Author: Juan Pablo Daza Medina  
	PBI : 7989
	Sprint : ERP_Services\SCM_week_14-16 (2025)
==============================================================================================================================*/

INSERT INTO Security.Form (Id, Name, PrintEvents, HasSequence, IsNativeForm,
		HasForm, ClassName, AssemblyName, HandlesMassiveConfirm, SequenceModule, State) 
	VALUES (@IdForm, 'Reporte de vacaciones', 'SUCA', 0, 1, 1, 
		'FrmReportVacation', 'Presentation.Payroll', 0, 'Payroll', 1);

INSERT INTO Security.ModuleForm (IdModule, IdTitle, IdForm, FormOrder)
	VALUES(53, 4, 2855, 1);

INSERT INTO Security.FormAction VALUES (2855, 11);
INSERT INTO Security.FormAction VALUES (2855, 22);
INSERT INTO Security.FormAction VALUES (2855, 23);
INSERT INTO Security.FormAction VALUES (2855, 24);
INSERT INTO Security.FormAction VALUES (2855, 25);
INSERT INTO Security.FormAction VALUES (2855, 41);
INSERT INTO Security.FormAction VALUES (2855, 70);
INSERT INTO Security.FormAction VALUES (2855, 114);

----------------------------------------------------------------  Sprint Week XX - XX (2026)  -----------------------------------------------------------------------
----------------------------------------------------------------  Sprint Week XX - XX (2026)  -----------------------------------------------------------------------
----------------------------------------------------------------  Sprint Week XX - XX (2026)  -----------------------------------------------------------------------
