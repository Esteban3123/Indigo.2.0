CREATE TABLE [az_func].[Leases_acd820f1709380fb_1706541213] (
    [NUMINGRES]                    CHAR (10)     NOT NULL,
    [_az_func_ChangeVersion]       BIGINT        NOT NULL,
    [_az_func_AttemptCount]        INT           NOT NULL,
    [_az_func_LeaseExpirationTime] DATETIME2 (7) NULL,
    PRIMARY KEY CLUSTERED ([NUMINGRES] ASC)
);

