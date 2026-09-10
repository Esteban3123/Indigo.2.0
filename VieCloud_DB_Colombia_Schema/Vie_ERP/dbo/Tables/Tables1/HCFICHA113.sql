CREATE TABLE [dbo].[HCFICHA113] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [PRIMNOMB]            VARCHAR (50)  NULL,
    [SEGNOMB]             VARCHAR (50)  NULL,
    [PRIMAPEL]            VARCHAR (50)  NULL,
    [SEGAPEL]             VARCHAR (50)  NULL,
    [TIPOID]              VARCHAR (50)  NULL,
    [NUMIDENT]            VARCHAR (50)  NULL,
    [NIVELEDU]            INT           NULL,
    [NUMNINOS]            VARCHAR (50)  NULL,
    [PESONACER]           VARCHAR (50)  NULL,
    [TALLNACER]           VARCHAR (50)  NULL,
    [EDADGEST]            VARCHAR (50)  NULL,
    [TIEMREC]             VARCHAR (50)  NULL,
    [EDADINICIO]          VARCHAR (50)  NULL,
    [INSCCREC]            BIT           NULL,
    [ESQVAC]              INT           NULL,
    [REFECARN]            BIT           NULL,
    [PESOACT]             VARCHAR (50)  NULL,
    [TALLACT]             VARCHAR (50)  NULL,
    [CIRCUNMED]           VARCHAR (50)  NULL,
    [EDEMA]               BIT           NULL,
    [DESNEMACIA]          BIT           NULL,
    [PIELRESEC]           BIT           NULL,
    [HIPOHIPER]           BIT           NULL,
    [CAMBCABEL]           BIT           NULL,
    [ANEMDETEC]           BIT           NULL,
    [ACTRUT]              BIT           NULL,
    [TIPOATEN]            BIT           NULL,
    [DIAGMEDI]            VARCHAR (MAX) NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA113] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA113_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA113_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA113] NOCHECK CONSTRAINT [CK_HCFICHA113_JSON];




GO
ALTER TABLE [dbo].[HCFICHA113] NOCHECK CONSTRAINT [CK_HCFICHA113_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales en formato JSON; almacena nuevas columnas y campos dinámicos de la ficha de notificación (VARCHAR MAX, validado con ISJSON)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación; valor nulo indica primera versión; formato V##_YYYY-MM-DD desde V01_2020-03-06', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnóstico médico registrado por el profesional de salud; texto libre del cuadro clínico y hallazgos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'DIAGMEDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diagnóstico Médico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'DIAGMEDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'DIAGMEDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de atención suministrada: True=Intrahospitalaria, False=Comunitaria; indica lugar de atención al paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TIPOATEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Atención Suministrada  True = IntrahospitalariaFalse = Comunitaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TIPOATEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TIPOATEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Activación de ruta de atención: True=Sí, False=No; indica si se activó protocolo de remisión o seguimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'ACTRUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Activación Ruta de Atención  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'ACTRUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'ACTRUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Anemia detectada por palidez palmar o de mucosas: True=Sí, False=No; signo clínico de desnutrición en menores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'ANEMDETEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Anemia Detectada por Palidez Palmar o de Mucosas    True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'ANEMDETEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'ANEMDETEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cambios en el cabello (opacidad, despigmentación, fragilidad): True=Sí, False=No; indicador nutricional pediátrico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'CAMBCABEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Cambios en el Cabello    True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'CAMBCABEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'CAMBCABEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hipo o hiperpigmentación de la piel: True=Sí, False=No; signo clínico asociado a deficiencias nutricionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'HIPOHIPER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'  Hipo o Hiperpigmentación de la Piel    True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'HIPOHIPER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'HIPOHIPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Piel reseca o áspera: True=Sí, False=No; indicador de deshidratación o desnutrición en niño', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'PIELRESEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Piel Reseca o Áspera  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'PIELRESEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'PIELRESEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Desnutrición, emaciación o delgadez visible: True=Sí, False=No; evaluación antropométrica clínica del menor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'DESNEMACIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Desnutrición Emaciación o Delgadez Visible  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'DESNEMACIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'DESNEMACIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edema presente en extremidades o cara: True=Sí, False=No; signo de malnutrición proteica en pediatría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'EDEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edema  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'EDEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'EDEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Circunferencia media del brazo en centímetros (cms); medida antropométrica para evaluar estado nutricional del menor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'CIRCUNMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Circunferencia Media del Brazo (Cms)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'CIRCUNMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'CIRCUNMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Talla actual en centímetros (cms); medida actual de longitud/estatura para seguimiento crecimiento y desarrollo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TALLACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Talla Actual (Cms)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TALLACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TALLACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso actual en kilogramos (kgs); medida actual para evaluación nutricional y crecimiento del paciente pediátrico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'PESOACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso Actual (Kgs)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'PESOACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'PESOACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referido por carné de vacunación: True=Sí, False=No; indica si la consulta fue derivada por hallazgo en esquema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'REFECARN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referido por Carné de Vacunación  True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'REFECARN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'REFECARN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Esquema de vacunación completo para edad: 1=Sí, 2=No, 3=Desconocido; cumplimiento del plan de inmunizaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'ESQVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esquema de Vacunación Completo a la Edad  1=Si  2= No  3= Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'ESQVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'ESQVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Inscrito a programa de crecimiento y desarrollo: True=Sí, False=No; afiliación a seguimiento pediátrico regular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'INSCCREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Inscrito a Crecimiento y Desarrollo  True = si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'INSCCREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'INSCCREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad de inicio de alimentación complementaria en meses; marca hito de transición alimentaria del lactante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'EDADINICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Inicio Alimentación Complementaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'EDADINICIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'EDADINICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo que recibió leche materna en meses; duración total de lactancia como indicador de nutrición infantil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TIEMREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo que Recibió Leche Materna (Meses)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TIEMREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TIEMREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad gestacional al nacer en semanas; semanas de gestación al parto, crítica para clasificación del neonato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'EDADGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Gestacional al Nacer (Semanas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'EDADGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'EDADGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Talla al nacer en centímetros (cms); medida de longitud al nacimiento, línea base antropométrica neonatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TALLNACER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Talla al Nacer (Cms)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TALLNACER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TALLNACER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso al nacer en gramos (grs); peso neonatal para evaluación prematuridad y adecuación para edad gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'PESONACER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso al Nacer (grs)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'PESONACER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'PESONACER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de niños menores de 5 años en el hogar; indica carga familiar y hacinamiento para contexto nutricional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'NUMNINOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Niños < 5 Años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'NUMNINOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'NUMNINOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel educativo de la madre o cuidador principal; INT código; asociado a conocimientos de nutrición infantil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'NIVELEDU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel Educativo de la Madre o Cuidador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'NIVELEDU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'NIVELEDU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación de la madre o cuidador; PII ofuscado; enlace a identidad del responsable del menor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'NUMIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'NUMIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'NUMIDENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de identificación de madre/cuidador: 1=RC, 2=TI, 3=CC, 4=CE, 5=PA, 6=MS, 7=AS, 8=PE; desde V01_2020-03-06', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TIPOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo ID (Documento Identificación) de la Madre --> desde versión ''''V01_2020-03-06'''' es con una enumeración:  1 = RC    2 = TI   3 = CC    4 = CE   5 = PA   6 = MS   7 = AS   8 = PE   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TIPOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TIPOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido de la madre o cuidador; componente de identificación nominal del responsable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'SEGAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo Apellido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'SEGAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'SEGAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido de la madre o cuidador; componente de identificación nominal del responsable del menor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'PRIMAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer apellido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'PRIMAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'PRIMAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre de la madre o cuidador; componente de identificación nominal del responsable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'SEGNOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sefundo Nombre ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'SEGNOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'SEGNOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre de la madre o cuidador; componente de identificación nominal de quien reporta al menor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'PRIMNOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer Nombre ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'PRIMNOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'PRIMNOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 (CHAR 4); clasificación médica de la condición evaluada en la ficha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de ficha de notificación (FK); referencia a evento o caso notificable en sistema de vigilancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID autoincremental (IDENTITY); identificador único de registro en tabla HCFICHA113', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación epidemiológica número 113 para seguimiento de desnutrición infantil. Registra datos del niño o niña (identificación, antropometría al nacer y actual, edad gestacional, esquema de vacunación, signos clínicos de desnutrición y diagnóstico médico) vinculados a una ficha de notificación y un diagnóstico CIE-10.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113';
