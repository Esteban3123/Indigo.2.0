CREATE TABLE [dbo].[INENTIDAD] (
    [CODENTIDA] CHAR (9)     NOT NULL,
    [NOMENTIDA] CHAR (250)   NULL,
    [CODIGONIT] CHAR (15)    NOT NULL,
    [CODADMPAG] CHAR (9)     NULL,
    [DIGITOVER] CHAR (1)     NOT NULL,
    [ENTDIRECC] CHAR (150)   NOT NULL,
    [ENTITELEF] CHAR (15)    NOT NULL,
    [ENTCONTAC] CHAR (250)   NULL,
    [ENTIEMAIL] CHAR (50)    NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_INEntidad] PRIMARY KEY CLUSTERED ([CODENTIDA] ASC)
);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INENTIDAD_6_1979922175__K1_2]
    ON [dbo].[INENTIDAD]([CODENTIDA] ASC)
    INCLUDE([NOMENTIDA]);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INENTIDAD_6_1979922175__K1_K2]
    ON [dbo].[INENTIDAD]([CODENTIDA] ASC, [NOMENTIDA] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_INENTIDAD_6_1979922175__K2_1]
    ON [dbo].[INENTIDAD]([NOMENTIDA] ASC)
    INCLUDE([CODENTIDA]);


GO
CREATE NONCLUSTERED INDEX [IX_INENTIDAD_CODIGONIT]
    ON [dbo].[INENTIDAD]([CODIGONIT] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'E-Mail', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'ENTIEMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'ENTCONTAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Telefono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'ENTITELEF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Direccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'ENTDIRECC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Digito de Verificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'DIGITOVER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Administradora Pagador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'CODADMPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'CODIGONIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'NOMENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INENTIDAD', @level2type = N'COLUMN', @level2name = N'CODENTIDA';

