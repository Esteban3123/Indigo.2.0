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
ALTER TABLE [HumanTalent].[SuggestionsClaimsMailbox] NOCHECK CONSTRAINT [FK_SuggestionsClaimsMailbox_Employee];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Portal que envía', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'MailingUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Envío', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'MailingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Empleado', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'EmployeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Anónimo:  0 - No  1 - Si', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'Anonymous';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tema', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'Topic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo:  1. Sugerencia  2. Queja  3. Reclamo  4. Peticiones  5. Felicitaciones', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'SuggestionsClaimsMailbox', @level2type = N'COLUMN', @level2name = N'Id';

