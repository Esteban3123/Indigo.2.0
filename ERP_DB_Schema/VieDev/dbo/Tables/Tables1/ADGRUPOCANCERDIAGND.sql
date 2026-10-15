CREATE TABLE [dbo].[ADGRUPOCANCERDIAGND] (
    [ID]               INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDADGRUPOCANCERC] INT      NOT NULL,
    [CODDIAGNO]        CHAR (4) NOT NULL,
    CONSTRAINT [PK_ADGRUPOCANCERDIAGND] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADGRUPOCANCERDIAGND_ADGRUPOCANCERC] FOREIGN KEY ([IDADGRUPOCANCERC]) REFERENCES [dbo].[ADGRUPOCANCERC] ([ID]),
    CONSTRAINT [FK_ADGRUPOCANCERDIAGND_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[ADGRUPOCANCERDIAGND] NOCHECK CONSTRAINT [FK_ADGRUPOCANCERDIAGND_ADGRUPOCANCERC];


GO
ALTER TABLE [dbo].[ADGRUPOCANCERDIAGND] NOCHECK CONSTRAINT [FK_ADGRUPOCANCERDIAGND_INDIAGNOS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERDIAGND', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el ID de grupos canceres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERDIAGND', @level2type = N'COLUMN', @level2name = N'IDADGRUPOCANCERC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERDIAGND', @level2type = N'COLUMN', @level2name = N'ID';

