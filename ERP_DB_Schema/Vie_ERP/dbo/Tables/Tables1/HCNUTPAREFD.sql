CREATE TABLE [dbo].[HCNUTPAREFD] (
    [ID]             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCNUTPAREC]   INT             NOT NULL,
    [IDHCPARNUTC]    INT             NOT NULL,
    [IDHCPARNUTFORD] INT             NOT NULL,
    [NAMEFORM]       CHAR (50)       NULL,
    [RESULT]         NUMERIC (18, 3) NOT NULL,
    [FORRESULT]      VARCHAR (5000)  NULL,
    [RRESULT]        VARCHAR (1000)  NOT NULL,
    [COLOR]          VARCHAR (20)    NULL,
    CONSTRAINT [PK__HCNUTPAR__3214EC2799E607CB] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCNUTPAREC_HCNUTPAREFD] FOREIGN KEY ([IDHCNUTPAREC]) REFERENCES [dbo].[HCNUTPAREC] ([ID]),
    CONSTRAINT [FK_HCPARNUTC_HCNUTPAREFD] FOREIGN KEY ([IDHCPARNUTC]) REFERENCES [dbo].[HCPARNUTC] ([ID]),
    CONSTRAINT [FK_HCPARNUTFORD_HCNUTPAREFD] FOREIGN KEY ([IDHCPARNUTFORD]) REFERENCES [dbo].[HCPARNUTFORD] ([ID])
);


GO
ALTER TABLE [dbo].[HCNUTPAREFD] NOCHECK CONSTRAINT [FK_HCNUTPAREC_HCNUTPAREFD];


GO
ALTER TABLE [dbo].[HCNUTPAREFD] NOCHECK CONSTRAINT [FK_HCPARNUTC_HCNUTPAREFD];


GO
ALTER TABLE [dbo].[HCNUTPAREFD] NOCHECK CONSTRAINT [FK_HCPARNUTFORD_HCNUTPAREFD];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el color ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'COLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formula reemplazada para calcular el valor de la formula farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'RRESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formula empleada para calcular el valor de la formula farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'FORRESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado de la formula farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'RESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la formula farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'NAMEFORM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la pametrizacion de las formulas farmaceuticas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'IDHCPARNUTFORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la pametrizacion de nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'IDHCPARNUTC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la cabecera de la prescripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'IDHCNUTPAREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNUTPAREFD', @level2type = N'COLUMN', @level2name = N'ID';

