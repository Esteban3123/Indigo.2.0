CREATE TABLE [Admissions].[DiagnosticsGroupsERCPrecursoras] (
    [Id]                     INT      IDENTITY (1, 1) NOT NULL,
    [IdGroupsERCPrecursoras] INT      NOT NULL,
    [DiagnosticCode]         CHAR (4) NOT NULL,
    CONSTRAINT [PK__Diagnost__3214EC0742A5BFFE] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DiagnosticsGroupsERCPrecursora_INDIAGNOS] FOREIGN KEY ([DiagnosticCode]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_DiagnosticsGroupsERCPrecursoras_GroupsERCPrecursoras] FOREIGN KEY ([IdGroupsERCPrecursoras]) REFERENCES [Admissions].[GroupsERCPrecursoras] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el codigo de diagnostico de la tabla INDIAGNOS', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'DiagnosticsGroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'DiagnosticCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el identificador unico de cada registro de la tabla GroupsERCPrecursoras', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'DiagnosticsGroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'IdGroupsERCPrecursoras';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el identificador unico de cada registro', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'DiagnosticsGroupsERCPrecursoras', @level2type = N'COLUMN', @level2name = N'Id';

