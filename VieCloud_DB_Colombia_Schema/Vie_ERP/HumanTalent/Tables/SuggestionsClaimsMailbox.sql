CREATE TABLE [HumanTalent].[SuggestionsClaimsMailbox] (
    [Id]          INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Type]        TINYINT        NOT NULL,
    [Topic]       VARCHAR (200)  NOT NULL,
    [Comments]    VARCHAR (1000) NOT NULL,
    [Anonymous]   BIT            NOT NULL,
    [EmployeeId]  INT            NULL,
    [MailingDate] DATETIME       NOT NULL,
    [MailingUser] VARCHAR (50)   NULL,
    CONSTRAINT [PK_SuggestionsClaimsMailbox] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SuggestionsClaimsMailbox_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de usuario del portal (VARCHAR 50) autenticado que envió la comunicación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'MailingUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de Portal que envía', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'MailingUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'MailingUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) exacta del envío de la comunicación al buzón', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'MailingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Envío', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'MailingDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'MailingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT) que referencia ID del empleado en tabla Payroll.Employee; nulo si anónimo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'EmployeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de anonimato (BIT): 0=No anónimo, 1=Anónimo; oculta identidad del remitente', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'Anonymous';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Anónimo:  0 - No  1 - Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'Anonymous';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'Anonymous';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada (VARCHAR 1000) y comentarios de la comunicación enviada', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tema o asunto (VARCHAR 200) de la sugerencia, queja, reclamo, petición o felicitación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'Topic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tema', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'Topic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'Topic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de comunicación (TINYINT): 1=Sugerencia, 2=Queja, 3=Reclamo, 4=Petición, 5=Felicitación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo:  1. Sugerencia  2. Queja  3. Reclamo  4. Peticiones  5. Felicitaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT) de registro en buzón de sugerencias, quejas y reclamos', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Buzón de sugerencias y reclamos del talento humano. Registra los mensajes enviados por empleados (o de forma anónima) con sugerencias, quejas o comentarios dirigidos al área de recursos humanos.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox';
