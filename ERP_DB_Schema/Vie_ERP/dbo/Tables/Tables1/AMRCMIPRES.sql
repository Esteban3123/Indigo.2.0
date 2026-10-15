CREATE TABLE [dbo].[AMRCMIPRES] (
    [ID]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC]     CHAR (10)     NOT NULL,
    [CODPRODUC]     CHAR (20)     NOT NULL,
    [FECHA]         DATETIME      NOT NULL,
    [JUSTIFICACION] VARCHAR (600) NOT NULL,
    CONSTRAINT [PK_AMRCMIPRES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AMRCMIPRES_HCJUNOPOM] FOREIGN KEY ([CODCONCEC], [CODPRODUC]) REFERENCES [dbo].[HCJUNOPOM] ([CODCONCEC], [CODPRODUC])
);


GO
ALTER TABLE [dbo].[AMRCMIPRES] NOCHECK CONSTRAINT [FK_AMRCMIPRES_HCJUNOPOM];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificacion de la habilitacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRES', @level2type = N'COLUMN', @level2name = N'JUSTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRES', @level2type = N'COLUMN', @level2name = N'FECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del medicamento Mipres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRES', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo habilitado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRES', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMRCMIPRES', @level2type = N'COLUMN', @level2name = N'ID';

