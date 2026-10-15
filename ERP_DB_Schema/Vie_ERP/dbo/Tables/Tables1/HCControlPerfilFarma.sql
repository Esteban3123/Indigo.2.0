CREATE TABLE [dbo].[HCControlPerfilFarma] (
    [ID]                INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [PERFILCAMBIOS]     BIT                                                                              NOT NULL,
    [FECHACREACION]     DATETIME                                                                         NOT NULL,
    [USUCREACION]       CHAR (20)                                                                        NOT NULL,
    [FECHAMODIFICACION] DATETIME                                                                         NULL,
    [USUMODIFICACION]   CHAR (20)                                                                        NULL,
    CONSTRAINT [PK_HCControlPerfilFarma] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCControlPerfilFarma_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCControlPerfilFarma] NOCHECK CONSTRAINT [FK_HCControlPerfilFarma_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'USUMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'USUCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perfil de cambios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'PERFILCAMBIOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCControlPerfilFarma', @level2type = N'COLUMN', @level2name = N'ID';

