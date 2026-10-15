CREATE TABLE [dbo].[ADPARCOEX] (
    [CODCENATE]              CHAR (10) NOT NULL,
    [REALIZAIN]              BIT       NOT NULL,
    [CODSERIPS]              CHAR (20) NULL,
    [ATPACCOEX]              BIT       NOT NULL,
    [DIALISING]              INT       NOT NULL,
    [ID]                     INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AllowPriorConsultation] INT       CONSTRAINT [DF_ADPARCOEX_AllowpriorConsultation] DEFAULT ((2)) NULL,
    CONSTRAINT [PK_ADPARCOEX] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADPARCOEX_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ADPARCOEX_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[ADPARCOEX] NOCHECK CONSTRAINT [FK_ADPARCOEX_ADCENATEN];


GO
ALTER TABLE [dbo].[ADPARCOEX] NOCHECK CONSTRAINT [FK_ADPARCOEX_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir pre-consulta    Si el parámetro se encuentra en "SI", se realizará lo siguiente:  1 - Se habilitará la pestaña y el formulario "Pre-consulta" en el formulario “Control citas consulta externa”.    2- Se habilitará la columna “Pre-consulta” en el formulario “Parametrización control de examen físico”.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'AllowPriorConsultation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCOEX', @level2type = N'COLUMN', @level2name = N'ID';

