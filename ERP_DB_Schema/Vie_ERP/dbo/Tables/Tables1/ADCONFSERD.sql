CREATE TABLE [dbo].[ADCONFSERD] (
    [ID]          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDADCONFSER] INT NOT NULL,
    [IDCaregroup] INT NOT NULL,
    CONSTRAINT [PK_ADCONFSERD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADCONFSERD_ADCONFSER] FOREIGN KEY ([IDADCONFSER]) REFERENCES [dbo].[ADCONFSER] ([CODCONCEC])
);


GO
ALTER TABLE [dbo].[ADCONFSERD] NOCHECK CONSTRAINT [FK_ADCONFSERD_ADCONFSER];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion de la tabla de VIE de Grupos de Atencion: Caregroup', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONFSERD', @level2type = N'COLUMN', @level2name = N'IDCaregroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla cabecera de los servicios suceptibles: ADCONFSER', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONFSERD', @level2type = N'COLUMN', @level2name = N'IDADCONFSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONFSERD', @level2type = N'COLUMN', @level2name = N'ID';

