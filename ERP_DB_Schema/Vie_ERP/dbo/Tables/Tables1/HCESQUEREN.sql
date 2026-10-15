CREATE TABLE [dbo].[HCESQUEREN] (
    [ID]        INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODESQUEM] NCHAR (10)  NOT NULL,
    [DESESQUEM] NCHAR (200) NOT NULL,
    [ESTADO]    TINYINT     NOT NULL,
    CONSTRAINT [PK_HCESQUEREN] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREN', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Esquema Renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREN', @level2type = N'COLUMN', @level2name = N'DESESQUEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del Esquema Renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREN', @level2type = N'COLUMN', @level2name = N'CODESQUEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del esquema renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREN', @level2type = N'COLUMN', @level2name = N'ID';

