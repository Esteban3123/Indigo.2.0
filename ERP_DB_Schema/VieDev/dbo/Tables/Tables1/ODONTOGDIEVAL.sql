CREATE TABLE [dbo].[ODONTOGDIEVAL] (
    [ID]           INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCONTROLVAL] INT     NOT NULL,
    [DIENTE]       TINYINT NOT NULL,
    CONSTRAINT [PK_ODONTOGDIEVAL__ID] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOGDIEVAL_ODONTOGCTRLVAL] FOREIGN KEY ([IDCONTROLVAL]) REFERENCES [dbo].[ODONTOGCTRLVAL] ([ID])
);


GO
ALTER TABLE [dbo].[ODONTOGDIEVAL] NOCHECK CONSTRAINT [FK_ODONTOGDIEVAL_ODONTOGCTRLVAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el diente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIEVAL', @level2type = N'COLUMN', @level2name = N'DIENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla ODONTOGCTRLVAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIEVAL', @level2type = N'COLUMN', @level2name = N'IDCONTROLVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOGDIEVAL', @level2type = N'COLUMN', @level2name = N'ID';

