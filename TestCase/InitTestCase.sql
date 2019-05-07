SELECT * FROM dbo.AbpUsers;
SELECT * FROM dbo.AbpUserClaims;
SELECT * FROM dbo.AbpUserLogins


SELECT * FROM dbo.IdentityServerApiResources;
SELECT * FROM dbo.IdentityServerApiClaims;
SELECT * FROM dbo.IdentityServerApiScopeClaims;
SELECT * FROM dbo.IdentityServerApiScopes;
SELECT * FROM dbo.IdentityServerApiSecrets;
SELECT * FROM dbo.IdentityServerClients;
SELECT * FROM dbo.IdentityServerClientScopes;
SELECT * FROM dbo.IdentityServerClientGrantTypes;
SELECT * FROM dbo.IdentityServerClientSecrets;
SELECT * FROM dbo.IdentityServerClientRedirectUris;
SELECT * FROM dbo.IdentityServerClientPostLogoutRedirectUris;
SELECT * FROM dbo.IdentityServerClientCorsOrigins;


SELECT * FROM dbo.IdentityServerIdentityResources;
SELECT * FROM dbo.IdentityServerIdentityClaims;

DELETE FROM dbo.AbpUsers;
DELETE FROM dbo.AbpUserClaims;

DELETE FROM dbo.IdentityServerApiResources;
DELETE FROM dbo.IdentityServerClients;

DELETE FROM dbo.IdentityServerIdentityResources;

INSERT INTO dbo.IdentityServerClientCorsOrigins
(
    ClientId,
    Origin
)
VALUES
(   '1BF1EAC6-18FA-4076-9702-8C57BCD7B428', -- ClientId - uniqueidentifier
    'http://localhost:5002'   -- Origin - nvarchar(150)
    )