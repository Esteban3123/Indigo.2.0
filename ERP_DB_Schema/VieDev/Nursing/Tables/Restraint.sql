CREATE TABLE [Nursing].[Restraint] (
    [Id]                      INT      IDENTITY (1, 1) NOT NULL,
    [IdTherapeuticRestraints] INT      NOT NULL,
    [IdTypeRestraint]         INT      NOT NULL,
    [Limb]                    TINYINT  NULL,
    [OrderDate]               DATETIME NOT NULL,
    [State]                   BIT      NOT NULL,
    [SuspendedDate]           DATETIME NULL,
    CONSTRAINT [PK_Restraint] PRIMARY KEY CLUSTERED ([Id] ASC)
);

