CREATE TABLE [dbo].[CONGRPUSC] (
    [IDGRUPUSC] TINYINT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CONNOMGRU] VARCHAR (50) NOT NULL,
    [CONNUMUSU] INT          NOT NULL,
    CONSTRAINT [PK_CONGRPUSC] PRIMARY KEY CLUSTERED ([IDGRUPUSC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CANTIDAD DE USUARIOS POR GRUPO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSC', @level2type = N'COLUMN', @level2name = N'CONNUMUSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSC', @level2type = N'COLUMN', @level2name = N'CONNOMGRU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo código grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSC', @level2type = N'COLUMN', @level2name = N'IDGRUPUSC';

