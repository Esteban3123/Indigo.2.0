CREATE TABLE [dbo].[HCREFCONTDET] (
    [ID]            INT                                                                          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCREFCONTAUTO] INT                                                                          NOT NULL,
    [CODDIAGNO]     CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)') NULL,
    CONSTRAINT [PK_HCREFCONTDET] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCREFCONTDET_HCREFCONT] FOREIGN KEY ([HCREFCONTAUTO]) REFERENCES [dbo].[HCREFCONT] ([AUTO]),
    CONSTRAINT [FK_HCREFCONTDET_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[HCREFCONTDET] NOCHECK CONSTRAINT [FK_HCREFCONTDET_HCREFCONT];


GO
ALTER TABLE [dbo].[HCREFCONTDET] NOCHECK CONSTRAINT [FK_HCREFCONTDET_INDIAGNOS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTDET', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla de referencia HCREFCONT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTDET', @level2type = N'COLUMN', @level2name = N'HCREFCONTAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFCONTDET', @level2type = N'COLUMN', @level2name = N'ID';

