CREATE TABLE [dbo].[ADDISCAPACI] (
    [DISCCODIGO] CHAR (3)   NOT NULL,
    [DISCDESCRI] CHAR (100) NULL,
    [ESTADO]     INT        NULL,
    CONSTRAINT [PK_ADDISCAPACI] PRIMARY KEY CLUSTERED ([DISCCODIGO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:  1-Activo  2-Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADDISCAPACI', @level2type = N'COLUMN', @level2name = N'ESTADO';

