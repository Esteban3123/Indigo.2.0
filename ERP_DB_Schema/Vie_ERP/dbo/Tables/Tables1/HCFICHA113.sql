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
ALTER TABLE [dbo].[HCFICHA113] NOCHECK CONSTRAINT [FK_HCFICHA113_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diagnóstico Médico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'DIAGMEDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Atención Suministrada  True = IntrahospitalariaFalse = Comunitaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TIPOATEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Activación Ruta de Atención  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'ACTRUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Anemia Detectada por Palidez Palmar o de Mucosas    True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'ANEMDETEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Cambios en el Cabello    True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'CAMBCABEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Hipo o Hiperpigmentación de la Piel    True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'HIPOHIPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Piel Reseca o Áspera  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'PIELRESEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desnutrición Emaciación o Delgadez Visible  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'DESNEMACIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edema  True = SiFalse = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'EDEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Circunferencia Media del Brazo (Cms)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'CIRCUNMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Talla Actual (Cms)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TALLACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso Actual (Kgs)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'PESOACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referido por Carné de Vacunación  True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'REFECARN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esquema de Vacunación Completo a la Edad  1=Si  2= No  3= Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'ESQVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Inscrito a Crecimiento y Desarrollo  True = si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'INSCCREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad Inicio Alimentación Complementaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'EDADINICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo que Recibió Leche Materna (Meses)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TIEMREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad Gestacional al Nacer (Semanas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'EDADGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Talla al Nacer (Cms)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TALLNACER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso al Nacer (grs)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'PESONACER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Niños < 5 Años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'NUMNINOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel Educativo de la Madre o Cuidador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'NIVELEDU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'NUMIDENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo ID (Documento Identificación) de la Madre --> desde versión ''V01_2020-03-06'' es con una enumeración:  1 = RC    2 = TI   3 = CC    4 = CE   5 = PA   6 = MS   7 = AS   8 = PE   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TIPOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo Apellido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'SEGAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer apellido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'PRIMAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sefundo Nombre ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'SEGNOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer Nombre ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'PRIMNOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'ID';

