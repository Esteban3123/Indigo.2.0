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
    CONSTRAINT [PK_HCPARACA] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la parametrizacion que indica si permite o no activar el proceso lactario.
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'LactationProcessActive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se crea el campo a raíz del desarrollo del PBI 25462.
Permitir registros de terapia sin orden médica:
1 - True
0 - False', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'AllowTherapyRecordsWithoutDoctorOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la parametrizacion que indica si permite autorizar mas de un ciclo en las historias clinicas    1 = Mas de 2 ciclos  2 = Mas de 3 ciclos  3= No permitir', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'AllowAuthorizingMTOCicleHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la parametrizacion que indica si permite o no la modificacion de los ciclos ya autorizados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'AllowAuthorizedCyclesModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para Habilitar Variables Cuenta Altocosto ERC  1 = si  2= no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'EnableCACERCVariablesInMedicalHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda las opciones seleccionadas en el parametro "Exige el consentimiento en órdenes médicas" en el formulario de Parametros de historias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'RequiresInformedConsent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para la generación de vista previa a impresión de reportes --> Solicitado por el PBI-11530 - Crear parámetro en centro de atención para determinar la manera de generación de reportes para impresion.     Se guarda con valores mayores a cero.   Actualmente correspondrá a los siguientes valores 1 y 2.     1 : Todos los reportes unificados.  2 : Individual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'PreviewAndPrintGeneration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para Habilitar variables cuenta de alto costo en HC médica  ---- Se agrega el 08/09/2023 solicitado por PBI 12257 - San José-EHR. 1- Crear parámetro Registrar variables CAC en formulario Parámetros de historias  ---  1. Si  0. No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'EnableHighCostAccountVariablesInMedicalHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite habiliar u ocultar las pestañas del plan manejo (Campo Temporal)  0 deja el plan manejo como siempre a estado (deja pestaña de laboratorio - images - patologias etc)  1. Habilitar solo una pestaña para (labo-pato-image-noqx-qx etc)      Esta columna NO tiene un campo en el formulario como tal es mas para temas tecnicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'NuevoPlanManejo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el pague aplicacones mezclas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'NuevoPageAplicacionMezclas';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que refleja la exigencia de la validación de tratamiento farmacológico cada 24 horas por medico:  SI  = 1   NO = 2 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'PharmacologicalTreatment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Columna obsoleta, funcionalidad anterior: (Campo que refleja la exigencia del consentimiento informado    No = 0   Si   = 1) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'RequireInformedConsentQx';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parametro que me va indicar si en triage se va a permitir registrar antecedentes ó si solo muestra el historico.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'DILIGANTECETRIAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Habilita indicaciones telefonicas   1 - si  2 - No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'HABINDICATELEFONICAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Precargar diagnóstico de cáncer en HC   SI  = 1   NO = 2 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'PRECARGARDIAGNOSTICOSCANCERHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 -  Si solicitar ciclo y dia  2 - No Solicitar ciclo y dia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'SOLCICLODIAQUIMIOTERAPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'URL para Consultar Historias Clinicas de Terceros.    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'URLCONSULTARHCTERCEROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la alerta incapacidad (true or false)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'ALERTAINCAPACIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Habilitar valor: "Sin especificar":  1  = si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CORRELASINESPECIFICAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Alertar por cantidades en cero en la orden médica:  1  = Si      0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CANTIDADESENCERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Correlación de paraclínicos obligatoria:   1  =  Si     0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'CORRELAPARACLIOBLIGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '- Orden de medicamento en custodia.    - Este campo en ''Si'', lo que hace es habilitarme un campo en el   formulario de la prescripción medica que dice si el "Paciente suministra medicamentos"   ese campo se habilita solo para Intrahospitalario y que este parametro este en ''Si''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'ORDENMEDICACUSTODIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de atención con servicios de urgencias  1 - Si  0 - No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'SERVIURGENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Valoraciones de seguimiento del Recién Nacido.    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'NUMSEGNAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Valoracion de seguimiento del Recién Nacido.  1-Indefinido  2-Definido    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARACA', @level2type = N'COLUMN', @level2name = N'VALSEGNAC';

