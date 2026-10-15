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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referido por Carné de Vacunación  True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'REFECARN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esquema de Vacunación Completo a la Edad  1=Si  2= No  3= Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'ESQVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Inscrito a Crecimiento y Desarrollo  True = si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'INSCCREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo ID (Documento Identificación) de la Madre --> desde versión ''V01_2020-03-06'' es con una enumeración:  1 = RC    2 = TI   3 = CC    4 = CE   5 = PA   6 = MS   7 = AS   8 = PE   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA113', @level2type = N'COLUMN', @level2name = N'TIPOID';

