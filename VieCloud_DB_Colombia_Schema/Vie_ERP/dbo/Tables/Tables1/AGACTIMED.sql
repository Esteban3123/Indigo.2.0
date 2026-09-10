CREATE TABLE [dbo].[AGACTIMED] (
    [CODACTMED]                         CHAR (3)       NOT NULL,
    [DESACTMED]                         NVARCHAR (150) NOT NULL,
    [ACTIVICON]                         CHAR (1)       NOT NULL,
    [DURAACTIV]                         CHAR (3)       NULL,
    [TIEMPOVAR]                         BIT            NULL,
    [CTRINTCIT]                         BIT            NULL,
    [NUMINTERV]                         CHAR (2)       NULL,
    [CTRCITASD]                         BIT            NULL,
    [NUMCITASD]                         CHAR (2)       NULL,
    [CTRCITASM]                         BIT            NULL,
    [NUMCITASM]                         CHAR (2)       NULL,
    [INDICAMED]                         NVARCHAR (250) NULL,
    [CONGRUPAL]                         BIT            NULL,
    [NUMPACMIN]                         CHAR (2)       NULL,
    [NUMPACMAX]                         CHAR (2)       NULL,
    [ESTADOACT]                         BIT            NOT NULL,
    [CODSERIPS]                         CHAR (20)      NULL,
    [R4505PLFAPRV]                      BIT            NULL,
    [R4505COPRPRV]                      BIT            NULL,
    [R4505COPRNPRV]                     BIT            NULL,
    [R4505CONOFT]                       BIT            NULL,
    [R4505CONNUT]                       BIT            NULL,
    [R4505CONPSI]                       BIT            NULL,
    [R4505CONCREDES]                    BIT            NULL,
    [R4505CONJOPRV]                     BIT            NULL,
    [R4505CONADPRV]                     BIT            NULL,
    [R4505NOAPL]                        BIT            CONSTRAINT [DF_AGACTIMED_R4505NOAPL] DEFAULT ((1)) NULL,
    [TIPRES256]                         TINYINT        NULL,
    [APLICARIAS]                        BIT            NULL,
    [URGENCIASODONTOLOGICA]             BIT            NULL,
    [IDRIASCUPS]                        INT            NULL,
    [PRIMECONTROLAUTOMATICO]            BIT            NULL,
    [CODSERIPSCONTROL]                  CHAR (20)      NULL,
    [DURAACTIVCONTROL]                  CHAR (3)       NULL,
    [TIEMPOVARCONTROL]                  BIT            NULL,
    [PAQDIALISIS]                       BIT            NULL,
    [MOSTRARWEB]                        BIT            NULL,
    [IDDESCRIPCIONRELACIONADA_CITA]     INT            NULL,
    [IDDESCRIPCIONRELACIONADA_CONTROL]  INT            NULL,
    [IDDESCRIPCIONRELACIONADA_DIALISIS] INT            NULL,
    [TIPOTRATAMIENTO]                   INT            NULL,
    [HemocomponentsRequest]             BIT            NULL,
    CONSTRAINT [PK_AGACTIMED] PRIMARY KEY CLUSTERED ([CODACTMED] ASC),
    CONSTRAINT [FK_AGACTIMED_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_AGACTIMED_RIAS] FOREIGN KEY ([IDRIASCUPS]) REFERENCES [dbo].[RIASCUPS] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si la actividad médica corresponde a transfusión de hemocomponentes, hemoderivados o productos sanguíneos. Búsqueda: transfusión, sangre, hemoderivados, hemocomponentes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'HemocomponentsRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establecer si la actividad medica, corresponde a transfusión de hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'HemocomponentsRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'HemocomponentsRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de tratamiento oncológico (INT): 1=Radioterapia Externa, 2=Braquiterapia, 3=Quimioterapia. Búsqueda: oncología, cáncer, radioterapia, quimio, tratamiento especial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'TIPOTRATAMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Radioterapia Externa  2 - Braquiterapia  3 - Quimioteapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'TIPOTRATAMIENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'TIPOTRATAMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) de descripción relacionada en VIE ERP (contract.CUPSEntityContractDescriptions) para actividades tipo diálisis, hemodiálisis, terapia renal. FK referencial. Búsqueda: diálisis, riñón, terapia renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_DIALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) - Actividad tipo Dialisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_DIALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_DIALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) de descripción relacionada en VIE ERP (contract.CUPSEntityContractDescriptions) para actividades de control médico general, seguimiento. FK referencial. Búsqueda: control, seguimiento, medicina general.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) - Actividad medicina general de control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) de descripción relacionada en VIE ERP (contract.CUPSEntityContractDescriptions) para actividades de medicina general, consulta externa. FK referencial. Búsqueda: cita, consulta, medicina general.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) - Actividad medicina general', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA_CITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si la cita/actividad se muestra en portal web: 1=Sí visible, 0=No visible. Búsqueda: web, portal, visibilidad, cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mostrar citas En Web  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si aplica a paquetes de tratamientos especiales y diálisis. Búsqueda: paquete, diálisis, tratamiento especial, oncología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'PAQDIALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Apl. a cons de paquetes de Ttos Especiales y Diálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'PAQDIALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'PAQDIALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si duración de control es variable/flexible: 1=Sí variable, 0=No fijo. Búsqueda: tiempo variable, duración, control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'TIEMPOVARCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duración de la actividad variable control  si  no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'TIEMPOVARCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'TIEMPOVARCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración en minutos (CHAR 3) de la cita de control médico, seguimiento. Búsqueda: duración, control, tiempo cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'DURAACTIVCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duración de la cita de control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'DURAACTIVCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'DURAACTIVCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS (CHAR 20) de la prestación de control médico, referencia a INCUPSIPS. FK referencial. Búsqueda: CUPS, código servicio, control.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CODSERIPSCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del CUPS de la cita de control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CODSERIPSCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CODSERIPSCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si el primer control se asigna automáticamente desde formulario de asignación de citas. Búsqueda: control automático, asignación, primera cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'PRIMECONTROLAUTOMATICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Me identifica si la primera vez y control es automatico desde el formulario de Asignación de citas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'PRIMECONTROLAUTOMATICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'PRIMECONTROLAUTOMATICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) de relación con tabla RIASCUPS, solo se completa si actividad aplica a RIAS. FK referencial. Búsqueda: RIAS, regulación, restricción internación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que me relaciona el ID de la RIASCUPS con la actividad de agendamiento, esto solamente se llena cuando la actividad Aplica a RIAS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si la actividad médica pertenece a urgencias odontológicas. Búsqueda: odontología, urgencia dental, dentista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'URGENCIASODONTOLOGICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para identificar si la actividad medica pertenece a una Urgencias Odontologicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'URGENCIASODONTOLOGICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'URGENCIASODONTOLOGICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si la actividad aplica para RIAS (solo cita médica o apoyo diagnóstico). Búsqueda: RIAS, regulación internación, restricción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'APLICARIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para identificar si la actividad aplica para RIAS, solo se habilita cuando es Cita medica ó apoyo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'APLICARIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'APLICARIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo Resolución 256 (TINYINT 0-12): medicina general, odontología, medicina interna, pediatría, ginecología, obstetricia, cirugía general, ecografía, RMN, cataratas, cadera, revascularización. Búsqueda: Res256, especialidad, clasificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'TIPRES256';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Res 256:   0: Ninguno   1:Medicina General   2:Odontología General   3:Medicina Interna   4:Pediatría   5:Ginecología   6:Obstetricía   7:Cirugía Géneral   8:Toma ecografía   9:Toma resonancía magnética nuclear   10:Cirugía de cataratas   11:Cirugía reemplazo de cadera   12:Cirugía de revascularización miocárdica   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'TIPRES256';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'TIPRES256';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si no aplica Resolución 4505. Default=1. Búsqueda: Res4505, no aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505NOAPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505NOAPL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505NOAPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión 73 Res4505: Consulta de Adulto Primera Vez. Búsqueda: consulta adulto, primera vez, Res4505.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONADPRV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(v.73) Consulta de Adulto Primera Vez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONADPRV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONADPRV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión 72 Res4505: Consulta de Joven Primera Vez. Búsqueda: consulta joven, primera vez, Res4505.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONJOPRV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(v.72) Consulta de Joven Primera Vez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONJOPRV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONJOPRV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión 69 Res4505: Consulta de Crecimiento y Desarrollo Primera Vez. Búsqueda: crecimiento desarrollo, pediatría, primera vez, Res4505.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONCREDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(v.69) Consulta de Crecimiento y Desrrollo Primera Vez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONCREDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONCREDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión 68 Res4505: Consulta de Psicología, salud mental. Búsqueda: psicología, salud mental, Res4505.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONPSI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(v.68) Consulta de Psicología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONPSI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONPSI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión 67 Res4505: Consulta de Nutrición, dietética. Búsqueda: nutrición, dietética, Res4505.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONNUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(v.67) Consulta de Nutrición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONNUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONNUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión 63 Res4505: Consulta por Oftalmología, óptica, ojos. Búsqueda: oftalmología, ojos, visión, Res4505.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONOFT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(v.63) Consulta por Oftalmología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONOFT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505CONOFT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión 57 Res4505: Control Prenatal No Primera Vez, seguimiento embarazo. Búsqueda: prenatal, embarazo, control, Res4505.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505COPRNPRV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(v.57) Control Prenatal (No Primera Vez)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505COPRNPRV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505COPRNPRV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión 56 Res4505: Control Prenatal Primera Vez, obstetricia inicial. Búsqueda: prenatal, embarazo, primera vez, Res4505.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505COPRPRV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(v.56) Control Prenatal de Priemera Vez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505COPRPRV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505COPRPRV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión 53 Res4505: Planificación Familiar Primera Vez, anticoncepción. Búsqueda: planificación familiar, anticoncepción, Res4505.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505PLFAPRV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(v.53) Planificación Familiar Primera Vez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505PLFAPRV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'R4505PLFAPRV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CUPS (CHAR 20) del servicio IPS, código de prestación. FK referencia a INCUPSIPS. Búsqueda: CUPS, código servicio, prestación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) del estado de la actividad: 1=Activo, 0=Inactivo, deshabilitado. Búsqueda: estado, activo, inactivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'ESTADOACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Actividad (Activo o inactivo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'ESTADOACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'ESTADOACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número máximo de pacientes (CHAR 2) en actividades grupales, consulta colectiva. Búsqueda: pacientes máximo, grupo, colectiva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'NUMPACMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Pacientes Maximo de la Actividad Grupal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'NUMPACMAX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'NUMPACMAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número mínimo de pacientes (CHAR 2) en actividades grupales, consulta colectiva. Búsqueda: pacientes mínimo, grupo, colectiva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'NUMPACMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Pacientes Minimo de la Actividad Grupal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'NUMPACMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'NUMPACMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si es actividad de consulta grupal, colectiva. Búsqueda: grupal, colectiva, grupo, consulta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CONGRUPAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Actividad de consulta Grupal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CONGRUPAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CONGRUPAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones médicas (NVARCHAR 250): criterios clínicos, diagnósticos, protocolos para la actividad. Búsqueda: indicación, diagnóstico, protocolo, criterio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'INDICAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones Medicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'INDICAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'INDICAMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de citas mensuales (CHAR 2) autorizadas para la actividad. Búsqueda: citas mensuales, cuota, límite mensual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'NUMCITASM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Citas Mensuales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'NUMCITASM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'NUMCITASM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si hay control de citas mensuales, límite mensual. Búsqueda: control mensual, cuota citas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CTRCITASM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control de citas Mensual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CTRCITASM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CTRCITASM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de citas diarias (CHAR 2) autorizadas para la actividad. Búsqueda: citas diarias, cuota, límite diario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'NUMCITASD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de Citas diarias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'NUMCITASD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'NUMCITASD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si hay control de citas diarias, límite por día. Búsqueda: control diario, cuota citas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CTRCITASD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control de citas Diarias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CTRCITASD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CTRCITASD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de días (CHAR 2) del intervalo entre citas, espaciamiento mínimo. Búsqueda: intervalo, espaciamiento citas, días.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'NUMINTERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del intervalo entre citas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'NUMINTERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'NUMINTERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si hay control de intervalo entre citas. Búsqueda: intervalo citas, espaciamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CTRINTCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Control para el intervalo de citas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CTRINTCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CTRINTCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si duración de la actividad es variable/flexible: 1=Variable, 0=Fija. Búsqueda: tiempo variable, duración flexible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'TIEMPOVAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo Variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'TIEMPOVAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'TIEMPOVAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración en minutos (CHAR 3) de la actividad, cita o consulta. Búsqueda: duración, tiempo cita, minutos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'DURAACTIV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duracion de la Actividad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'DURAACTIV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'DURAACTIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de actividad (CHAR 1): 0=Cita médica/consulta externa, 1=Procedimiento quirúrgico, 2=Apoyo diagnóstico/terapéutico, 3=Oncología/especiales, 4=Diálisis, 5=Mixta. Búsqueda: tipo actividad, cita, procedimiento, diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'ACTIVICON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Actividad de Consulta  0: Cita médica ó consulta externa  1: Procedimiento Qx  2: Otros Procedimientos (Apoyo Diagnóstico-terapeutico-otros procedimientos)  3: Tratamientos oncológicos ó especiales  4: Diálisis  5: Mixta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'ACTIVICON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'ACTIVICON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la actividad médica (NVARCHAR 150): nombre, título de la prestación, servicio. Búsqueda: nombre actividad, prestación, descripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'DESACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la actividad Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'DESACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'DESACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la actividad médica (CHAR 3), identificador único PK. Búsqueda: código actividad, ID prestación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Actividad Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de actividades médicas configuradas para agendamiento de citas. Define los tipos de consulta, procedimiento o intervención que pueden programarse, incluyendo duración, capacidad de pacientes, controles automáticos, indicaciones clínicas y clasificación por servicio (CUPS).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMED';
