CREATE TABLE [dbo].[HCCOMSAN] (
    [ID]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCOMSAM]                   VARCHAR (20)    NOT NULL,
    [DESCOMSAM]                   VARCHAR (200)   NOT NULL,
    [VALIDAHEMOCLA]               INT             NULL,
    [MinimumContentHemocomponent] NUMERIC (18, 1) NULL,
    [MaximumContentHemocomponent] NUMERIC (18, 1) NULL,
    [State]                       BIT             NULL,
    CONSTRAINT [PK__HCCOMSAN__DE084E6E6079D22F] PRIMARY KEY CLUSTERED ([ID] ASC)
);

