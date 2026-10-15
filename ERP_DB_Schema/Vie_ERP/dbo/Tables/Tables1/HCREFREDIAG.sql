CREATE TABLE [dbo].[HCREFREDIAG] (
    [ID]           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [INDIAGNOSCOD] CHAR (4)      NOT NULL,
    [PRINCIPAL]    BIT           NOT NULL,
    [OBSERVACION]  VARCHAR (200) NULL,
    [HCREFRECEPID] INT           NOT NULL,
    CONSTRAINT [PK_HCREFREDIAG] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCREFREDIAG_INDIAGNOS] FOREIGN KEY ([INDIAGNOSCOD]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[HCREFREDIAG] NOCHECK CONSTRAINT [FK_HCREFREDIAG_INDIAGNOS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG', @level2type = N'COLUMN', @level2name = N'HCREFRECEPID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si el diagnostico es o no principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG', @level2type = N'COLUMN', @level2name = N'PRINCIPAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG', @level2type = N'COLUMN', @level2name = N'INDIAGNOSCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREFREDIAG', @level2type = N'COLUMN', @level2name = N'ID';

