CREATE TABLE [dbo].[CARTERA_ESTADO] (
    [ID]     INT       IDENTITY (1, 1) NOT NULL,
    [CUENTA] CHAR (20) NULL,
    [ESTADO] CHAR (20) NULL,
    CONSTRAINT [PK_CARTERA_ESTADO] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_ESTADO', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_ESTADO', @level2type = N'COLUMN', @level2name = N'CUENTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CARTERA_ESTADO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla que registra el estado asociado a cada cuenta de cartera (módulo de cuentas por cobrar o facturación). Almacena el identificador de la cuenta y su estado correspondiente, permitiendo rastrear en qué situación se encuentra cada cuenta dentro del proceso de gestión de cartera.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CARTERA_ESTADO';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CARTERA_ESTADO';
GO
