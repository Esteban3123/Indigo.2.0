CREATE TABLE [dbo].[AGENTURNO] (
    [ID]         INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC]  INT      NOT NULL,
    [NUMTURNO]   INT      NULL,
    [TURHORAINI] DATETIME NULL,
    [TURHORAFIN] DATETIME NULL,
    CONSTRAINT [PK_AGENTURNO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGENTURNO_AGENSALAC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[AGENSALAC] ([CODCONCEC])
);


GO
ALTER TABLE [dbo].[AGENTURNO] NOCHECK CONSTRAINT [FK_AGENTURNO_AGENSALAC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora de Fin del Turno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO', @level2type = N'COLUMN', @level2name = N'TURHORAFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora de Inicio del Turno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO', @level2type = N'COLUMN', @level2name = N'TURHORAINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número del turno (1,2,3,4,5 o 6)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO', @level2type = N'COLUMN', @level2name = N'NUMTURNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del concecutivo de la Sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO', @level2type = N'COLUMN', @level2name = N'ID';

