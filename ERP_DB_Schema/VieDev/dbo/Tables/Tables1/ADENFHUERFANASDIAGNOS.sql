CREATE TABLE [dbo].[ADENFHUERFANASDIAGNOS] (
    [ID]               INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDADENFHUERFANAS] INT      NOT NULL,
    [CODDIAGNO]        CHAR (4) NOT NULL,
    CONSTRAINT [PK_ADENFHUERFANASDIAGNOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADENFHUERFANASDIAGNOS_ADENFHUERFANAS] FOREIGN KEY ([IDADENFHUERFANAS]) REFERENCES [dbo].[ADENFHUERFANAS] ([ID]),
    CONSTRAINT [FK_ADENFHUERFANASDIAGNOS_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[ADENFHUERFANASDIAGNOS] NOCHECK CONSTRAINT [FK_ADENFHUERFANASDIAGNOS_ADENFHUERFANAS];


GO
ALTER TABLE [dbo].[ADENFHUERFANASDIAGNOS] NOCHECK CONSTRAINT [FK_ADENFHUERFANASDIAGNOS_INDIAGNOS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda elcodigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANASDIAGNOS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el ID de las enfermedades huerfanas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANASDIAGNOS', @level2type = N'COLUMN', @level2name = N'IDADENFHUERFANAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADENFHUERFANASDIAGNOS', @level2type = N'COLUMN', @level2name = N'ID';

