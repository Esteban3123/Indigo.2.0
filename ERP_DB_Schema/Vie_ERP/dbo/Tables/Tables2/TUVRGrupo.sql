CREATE TABLE [dbo].[TUVRGrupo] (
    [CODIGO] CHAR (10) NOT NULL,
    [UVR]    CHAR (15) NOT NULL,
    [Grupo]  CHAR (5)  NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TUVRGrupo', @level2type = N'COLUMN', @level2name = N'Grupo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TUVRGrupo', @level2type = N'COLUMN', @level2name = N'UVR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TUVRGrupo', @level2type = N'COLUMN', @level2name = N'CODIGO';

