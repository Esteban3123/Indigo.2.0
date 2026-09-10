CREATE TABLE [dbo].[HCPARACA] (
    [ID]                                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]                                 CHAR (10)     NOT NULL,
    [VALSEGNAC]                                 INT           CONSTRAINT [DF_HCPARACA_VALSEGNAC] DEFAULT ((1)) NOT NULL,
    [NUMSEGNAC]                                 INT           NULL,
    [SERVIURGENCIA]                             BIT           CONSTRAINT [DF_HCPARACA_SERVIURGENCIA] DEFAULT ((0)) NOT NULL,
    [ORDENMEDICACUSTODIA]                       BIT           NULL,
    [CORRELAPARACLIOBLIGA]                      BIT           CONSTRAINT [DF_CORRELAPARACLIOBLIGA] DEFAULT ((0)) NOT NULL,
    [CANTIDADESENCERO]                          BIT           NULL,
    [CORRELASINESPECIFICAR]                     BIT           CONSTRAINT [DF_CORRELASINESPECIFICAR] DEFAULT ((0)) NOT NULL,
    [ALERTAINCAPACIDAD]                         BIT           CONSTRAINT [DF_ALERTAINCAPACIDAD] DEFAULT ((0)) NOT NULL,
    [JUSTIMEDNOPOS]                             BIT           NULL,
    [INTERESPEINFECTO]                          CHAR (3)      NULL,
    [CODSERINT]                                 CHAR (20)     NULL,
    [IDDESCRIPCIONRELACIONADA_INTER]            INT           NULL,
    [SIMULABRAQUITERAPIA]                       BIT           NULL,
    [VALIDAAUTORIZACIONINTRAHOS]                BIT           NULL,
    [VALIDAAUTORIZACIONEXTRA]                   BIT           NULL,
    [CONSULTARHCTERCEROS]                       BIT           NULL,
    [URLCONSULTARHCTERCEROS]                    VARCHAR (150) NULL,
    [PROVEEDORTERCEROS]                         VARCHAR (100) NULL,
    [SOLCICLODIAQUIMIOTERAPIA]                  INT           NULL,
    [PRECARGARDIAGNOSTICOSCANCERHC]             INT           NULL,
    [HABINDICATELEFONICAS]                      BIT           NULL,
    [DILIGANTECETRIAGE]                         BIT           NULL,
    [RequireInformedConsentQx]                  INT           CONSTRAINT [DF_HCPARACA_RequireInformedConsentQx] DEFAULT ((0)) NOT NULL,
    [PharmacologicalTreatment]                  INT           NULL,
    [NuevoPageAplicacionMezclas]                BIT           NULL,
    [AllowDrugReformulation]                    BIT           CONSTRAINT [DF__HCPARACA__AllowD__1773DBCC] DEFAULT ((0)) NULL,
    [Extramuralapplicationvalidity]             VARCHAR (200) NULL,
    [AllowConfirmOncologialSchemesCentralMixes] BIT           NULL,
    [NuevoPlanManejo]                           BIT           NULL,
    [EnableHighCostAccountVariablesInMedicalHC] BIT           NULL,
    [PreviewAndPrintGeneration]                 INT           NULL,
    [RequiresInformedConsent]                   VARCHAR (MAX) NULL,
    [EnableCACERCVariablesInMedicalHC]          BIT           NULL,
    [AllowAuthorizedCyclesModification]         BIT           NULL,
    [AllowAuthorizingMTOCicleHC]                INT           NULL,
    [AllowTherapyRecordsWithoutDoctorOrder]     BIT           DEFAULT ((1)) NOT NULL,
    [LactationProcessActive]                    BIT           NULL,
    [DietOrderRequired]                         BIT           NULL,
    [SurgicalProcedureCACVariables]             INT           NULL,
    [DxPresumptiveCancer]                       INT           NULL,
    [CkbxDxPresumptiveCancer]                   BIT           NULL,
    [AllowGrowthCurvevisualization]             BIT           CONSTRAINT [HCPARACA_AllowGrowthCurvevisualization] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_HCPARACA] PRIMARY KEY CLUSTERED ([ID] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Parámetro que habilita o inhabilita el proceso lactario en la historia clínica (1=Activo, 0=Inactivo). Aplica a recién nacidos y lactancia materna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'LactationProcessActive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la parametrizacion que indica si permite o no activar el proceso lactario.
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'LactationProcessActive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'LactationProcessActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Permite registrar sesiones de terapia (física, ocupacional, del lenguaje) sin requerir orden médica previa (1=Sí, 0=No). PBI 25462.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'AllowTherapyRecordsWithoutDoctorOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se crea el campo a raíz del desarrollo del PBI 25462.
Permitir registros de terapia sin orden médica:
1 - True
0 - False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'AllowTherapyRecordsWithoutDoctorOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'AllowTherapyRecordsWithoutDoctorOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Controla autorización de ciclos múltiples en tratamientos oncológicos/quimioterapia (1=Más de 2 ciclos, 2=Más de 3 ciclos, 3=No permitir).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'AllowAuthorizingMTOCicleHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la parametrizacion que indica si permite autorizar mas de un ciclo en las historias clinicas    1 = Mas de 2 ciclos  2 = Mas de 3 ciclos  3= No permitir', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'AllowAuthorizingMTOCicleHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'AllowAuthorizingMTOCicleHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Habilita o inhabilita la modificación de ciclos ya autorizados en tratamientos oncológicos (1=Permitir, 0=Bloquear).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'AllowAuthorizedCyclesModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la parametrizacion que indica si permite o no la modificacion de los ciclos ya autorizados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'AllowAuthorizedCyclesModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'AllowAuthorizedCyclesModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Habilita registro de variables CAC (Cuenta Alto Costo) en historia clínica para pacientes ERC (1=Sí, 0=No).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'EnableCACERCVariablesInMedicalHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para Habilitar Variables Cuenta Altocosto ERC  1 = si  2= no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'EnableCACERCVariablesInMedicalHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'EnableCACERCVariablesInMedicalHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(MAX). Almacena configuración de exigencia de consentimiento informado en órdenes médicas, procedimientos y autorizaciones de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'RequiresInformedConsent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda las opciones seleccionadas en el parametro "Exige el consentimiento en órdenes médicas" en el formulario de Parametros de historias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'RequiresInformedConsent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'RequiresInformedConsent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Define modo de generación de reportes para impresión en historia clínica (1=Reporte unificado, 2=Reportes individuales). PBI-11530.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'PreviewAndPrintGeneration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para la generación de vista previa a impresión de reportes --> Solicitado por el PBI-11530 - Crear parámetro en centro de atención para determinar la manera de generación de reportes para impresion.     Se guarda con valores mayores a cero.   Actualmente correspondrá a los siguientes valores 1 y 2.     1 : Todos los reportes unificados.  2 : Individual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'PreviewAndPrintGeneration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'PreviewAndPrintGeneration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Habilita registro de variables CAC en historia clínica médica para pacientes alto costo (1=Sí, 0=No). PBI 12257.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'EnableHighCostAccountVariablesInMedicalHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para Habilitar variables cuenta de alto costo en HC médica  ---- Se agrega el 08/09/2023 solicitado por PBI 12257 - San José-EHR. 1- Crear parámetro Registrar variables CAC en formulario Parámetros de historias  ---  1. Si  0. No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'EnableHighCostAccountVariablesInMedicalHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'EnableHighCostAccountVariablesInMedicalHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Parámetro técnico que habilita nueva interfaz de plan de manejo con pestañas segregadas (laboratorio, patología, imagen, procedimientos Qx) en historia clínica (0=Clásico, 1=Nuevo).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'NuevoPlanManejo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite habiliar u ocultar las pestañas del plan manejo (Campo Temporal)  0 deja el plan manejo como siempre a estado (deja pestaña de laboratorio - images - patologias etc)  1. Habilitar solo una pestaña para (labo-pato-image-noqx-qx etc)      Esta columna NO tiene un campo en el formulario como tal es mas para temas tecnicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'NuevoPlanManejo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'NuevoPlanManejo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Permite interfaz automática de medicamentos de quimioterapia con central de mezclas (1=Sí, 0=No).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'AllowConfirmOncologialSchemesCentralMixes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'parametro por tabla para saber si los medicamentos de quimioterapia realicen interfaz automatica con la central de mezclas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'AllowConfirmOncologialSchemesCentralMixes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'AllowConfirmOncologialSchemesCentralMixes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(200). Vigencia o período de validez de solicitudes de atención extramural, interconsultas y procedimientos fuera de sede.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'Extramuralapplicationvalidity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vigencia solicitud extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'Extramuralapplicationvalidity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'Extramuralapplicationvalidity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Habilita reformulación de medicamentos, mezclas y líquidos cuando la especialidad del médico actual coincide con la del prescriptor original (1=Sí, 0=No).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'AllowDrugReformulation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite habilitar reformulacion de medicamentos y mezclas-liquidos en caso de que la especialidad del medico actual sea igual al del la H', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'AllowDrugReformulation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'AllowDrugReformulation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Parámetro técnico que activa nueva página de aplicación de mezclas en módulo farmacéutico de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'NuevoPageAplicacionMezclas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el pague aplicacones mezclas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'NuevoPageAplicacionMezclas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'NuevoPageAplicacionMezclas';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Exige validación de tratamiento farmacológico cada 24 horas por médico tratante en historia clínica (1=Sí, 2=No).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'PharmacologicalTreatment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que refleja la exigencia de la validación de tratamiento farmacológico cada 24 horas por medico:  SI  = 1   NO = 2 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'PharmacologicalTreatment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'PharmacologicalTreatment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Columna obsoleta. Anterior control de exigencia de consentimiento informado para procedimientos quirúrgicos (0=No, 1=Sí).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'RequireInformedConsentQx';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna obsoleta, funcionalidad anterior: (Campo que refleja la exigencia del consentimiento informado    No = 0   Si   = 1) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'RequireInformedConsentQx';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'RequireInformedConsentQx';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Parámetro de triage que controla si permite registrar antecedentes nuevos o solo visualiza histórico del paciente (1=Permitir registro, 0=Solo lectura).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'DILIGANTECETRIAGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parametro que me va indicar si en triage se va a permitir registrar antecedentes ó si solo muestra el historico.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'DILIGANTECETRIAGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'DILIGANTECETRIAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Habilita indicaciones telefónicas en ordenes médicas y prescripciones (1=Sí, 2=No).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'HABINDICATELEFONICAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Habilita indicaciones telefonicas   1 - si  2 - No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'HABINDICATELEFONICAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'HABINDICATELEFONICAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Precarga diagnósticos de cáncer en nueva historia clínica oncológica (1=Sí, 2=No).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'PRECARGARDIAGNOSTICOSCANCERHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Precargar diagnóstico de cáncer en HC   SI  = 1   NO = 2 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'PRECARGARDIAGNOSTICOSCANCERHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'PRECARGARDIAGNOSTICOSCANCERHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Controla si solicita especificación de ciclo y día en prescripción de quimioterapia (1=Solicitar, 2=No solicitar).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'SOLCICLODIAQUIMIOTERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 -  Si solicitar ciclo y dia  2 - No Solicitar ciclo y dia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'SOLCICLODIAQUIMIOTERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'SOLCICLODIAQUIMIOTERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(100). Nombre o código del proveedor de historias clínicas externas/terceros para interconsulta y continuidad de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'PROVEEDORTERCEROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Proveedor de las Historias clinicas de Terceros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'PROVEEDORTERCEROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'PROVEEDORTERCEROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(150). URL de interfaz o API para consultar historias clínicas de terceros, otras IPS o centros de atención externos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'URLCONSULTARHCTERCEROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'URL para Consultar Historias Clinicas de Terceros.    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'URLCONSULTARHCTERCEROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'URLCONSULTARHCTERCEROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Habilita consulta de historias clínicas de terceros/externa (1=Permitir, 0=Bloquear).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CONSULTARHCTERCEROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si se puede Consultar Historias Clinicas de Terceros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CONSULTARHCTERCEROS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CONSULTARHCTERCEROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Valida autorización de órdenes médicas para atención extramural antes de agendamiento o asignación de citas (1=Sí, 0=No).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'VALIDAAUTORIZACIONEXTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para saber si se valida la autorizacion de las ordenes medicas Extramurales (inicialmente en agendamiento proceso asignacion citas trta especiales)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'VALIDAAUTORIZACIONEXTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'VALIDAAUTORIZACIONEXTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Valida autorización de órdenes médicas intrahospitalarias antes de agendamiento o asignación de citas tratamientos especiales (1=Sí, 0=No).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'VALIDAAUTORIZACIONINTRAHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para saber si se valida la autorizacion de las ordenes medicas intrahospitalaria (inicialmente en agendamiento proceso asignacion citas trta especiales)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'VALIDAAUTORIZACIONINTRAHOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'VALIDAAUTORIZACIONINTRAHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Columna obsoleta. Anterior parámetro para simular braquiterapia (radioterapia interna). Funcionalidad descontinuada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'SIMULABRAQUITERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obsoleta ya que braquiterapia se separo de radio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'SIMULABRAQUITERAPIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'SIMULABRAQUITERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. FK a VIE_ERP.contract.CUPSEntityContractDescriptions. ID de descripción de servicio/procedimiento para interconsulta y CUPS asociado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_INTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacionada . relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) - CUPS interconsulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_INTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_INTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(20). Código único CUPS (Código Único de Procedimientos y Servicios) para servicios de interconsulta y procedimientos especializados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CODSERINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios para Interconsultas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CODSERINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CODSERINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(3). Código de especialidad para interconsulta en Infectología o servicios de control de infecciones intrahospitalarias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'INTERESPEINFECTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código Interconsulta Especialidad por Infectología --> Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'INTERESPEINFECTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'INTERESPEINFECTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Exige justificación clínica para medicamentos no incluidos en POS (1=Requerido, 0=Opcional).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'JUSTIMEDNOPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que recibe si se debe tener justificacion de medicamentos no POS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'JUSTIMEDNOPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'JUSTIMEDNOPOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Activa alerta en historia clínica cuando se genera incapacidad laboral (1=Activo, 0=Inactivo).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'ALERTAINCAPACIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la alerta incapacidad (true or false)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'ALERTAINCAPACIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'ALERTAINCAPACIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Habilita opción ''''Sin especificar'''' en correlación de paraclínicos (1=Sí, 0=No).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CORRELASINESPECIFICAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Habilitar valor: "Sin especificar":  1  = si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CORRELASINESPECIFICAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CORRELASINESPECIFICAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Emite alerta de cantidades en cero en prescripciones médicas (1=Alertar, 0=No alertar).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CANTIDADESENCERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Alertar por cantidades en cero en la orden médica:  1  = Si      0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CANTIDADESENCERO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CANTIDADESENCERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Hace obligatoria la correlación/justificación de resultados paraclínicos con diagnóstico (1=Obligatorio, 0=Opcional).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CORRELAPARACLIOBLIGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Correlación de paraclínicos obligatoria:   1  =  Si     0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CORRELAPARACLIOBLIGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CORRELAPARACLIOBLIGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Habilita opción en prescripción médica de medicamentos en custodia, permitiendo marcar si el paciente suministra medicamentos intrahospitalarios (1=Sí, 0=No).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'ORDENMEDICACUSTODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'- Orden de medicamento en custodia.    - Este campo en ''''Si'''', lo que hace es habilitarme un campo en el   formulario de la prescripción medica que dice si el "Paciente suministra medicamentos"   ese campo se habilita solo para Intrahospitalario y que este parametro este en ''''Si''''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'ORDENMEDICACUSTODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'ORDENMEDICACUSTODIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indica si el centro de atención cuenta con servicio de urgencias/emergencias (1=Sí, 0=No).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'SERVIURGENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de atención con servicios de urgencias  1 - Si  0 - No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'SERVIURGENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'SERVIURGENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Número de valoraciones de seguimiento programadas para recién nacido después de nacimiento y hasta alta (Control de crecimiento y desarrollo).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'NUMSEGNAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Valoraciones de seguimiento del Recién Nacido.    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'NUMSEGNAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'NUMSEGNAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Tipo de valoración de seguimiento del recién nacido (1=Indefinido, 2=Definido).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'VALSEGNAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Valoracion de seguimiento del Recién Nacido.  1-Indefinido  2-Definido    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'VALSEGNAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'VALSEGNAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(10). Código único del centro/sede de atención en el sistema (FK a maestro de centros, unidades funcionales).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT IDENTITY. Consecutivo único y clave primaria de la tabla de configuración de historia clínica por centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Especifica desde dónde se registran variables CAC en procedimientos quirúrgicos (1=Desde informe Qx, 2=Desde valoración médica).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'SurgicalProcedureCACVariables';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la variable "Registro de variables CAC de procedimientos Qx en:"
1 - Desde informe Qx
2 - Desde valoración médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'SurgicalProcedureCACVariables';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'SurgicalProcedureCACVariables';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el valor de días de diagnóstico presuntivo de cancer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'DxPresumptiveCancer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Exige prescripción de dieta en historia clínica para hospitalización (1=Requerido, 0=Opcional).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'DietOrderRequired';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo En historia clínica exige prescripción de dieta.
True = Si
False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'DietOrderRequired';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'DietOrderRequired';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el valor del checkbox vigencias diagnóstico presuntivo de cancer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CkbxDxPresumptiveCancer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de configuración del módulo de paraclínicos (exámenes, laboratorios, imágenes y procedimientos) por centro de atención. Controla reglas de negocio como validación de autorizaciones, correlación obligatoria, manejo oncológico, consentimientos informados, farmacia y urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA';
