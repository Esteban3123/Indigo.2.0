CREATE TABLE [dbo].[HCCAUSAPCE] (
    [CODCAUSAPCE] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMCAUSAPCE] VARCHAR (100) NOT NULL,
    [ESTADO]      INT           NULL,
    CONSTRAINT [PK_HCCAUSAPCE] PRIMARY KEY CLUSTERED ([CODCAUSAPCE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 = Activo   2 = Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCAUSAPCE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del causas de diagnosticos de valoracion del plan cuidado de enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCAUSAPCE', @level2type = N'COLUMN', @level2name = N'NOMCAUSAPCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la causa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCAUSAPCE', @level2type = N'COLUMN', @level2name = N'CODCAUSAPCE';

