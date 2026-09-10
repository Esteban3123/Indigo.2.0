CREATE TABLE [dbo].[RazonesRetiro] (
    [Codigo]        VARCHAR (50) NULL,
    [Nombre]        VARCHAR (50) NULL,
    [Indemnizacion] BIT          NULL,
    [Estado]        BIT          NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Catálogo de razones o motivos por los cuales un empleado o persona puede ser retirado o desvinculado. Cada razón incluye un indicador que señala si genera derecho a indemnización y un estado activo/inactivo para controlar su vigencia en el sistema.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RazonesRetiro';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RazonesRetiro';
GO
