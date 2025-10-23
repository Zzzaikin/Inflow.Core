-- we don't know how to generate root <with-no-name> (class Root) :(

grant connect on database :: TestInflow to dbo
go

grant view any column encryption key definition, view any column master key definition on database :: TestInflow to [public]
go

create table Contact
(
    Id   uniqueidentifier default newid() not null
        constraint Contact_pk
            primary key,
    Name nvarchar(250)                    not null
)
    go

create table [User]
(
    Id         uniqueidentifier default newid() not null
    constraint User_pk
    primary key,
    Name       nvarchar(250)                    not null,
    CreatedOn  datetime2        default getutcdate(),
    ModifiedOn datetime2        default getutcdate(),
    ContactId  uniqueidentifier                 not null
    constraint User_Contact_Id_fk
    references Contact,
    Active     bit
    )
    go

create table [Order]
(
    Id          uniqueidentifier default newid() not null
    constraint Order_pk
    primary key,
    Name        nvarchar(250)                    not null,
    Description nvarchar(250),
    UserId      uniqueidentifier                 not null
    constraint Order_User_Id_fk
    references [User]
    )
    go

    use master
    go

    grant connect sql to ##MS_AgentSigningCertificate##
    go

    use TestInflow
    go

    use master
    go

    grant connect sql to ##MS_PolicyEventProcessingLogin##
    go

    use TestInflow
    go

    use master
    go

    grant control server, view any definition to ##MS_PolicySigningCertificate##
    go

    use TestInflow
    go

    use master
    go

    grant connect sql, view any definition, view server state to ##MS_PolicyTsqlExecutionLogin##
    go

    use TestInflow
    go

    use master
    go

    grant authenticate server to ##MS_SQLAuthenticatorCertificate##
    go

    use TestInflow
    go

    use master
    go

    grant authenticate server, view any definition, view server state to ##MS_SQLReplicationSigningCertificate##
    go

    use TestInflow
    go

    use master
    go

    grant view any definition to ##MS_SQLResourceSigningCertificate##
    go

    use TestInflow
    go

    use master
    go

    grant view any definition to ##MS_SmoExtendedSigningCertificate##
    go

    use TestInflow
    go

    use master
    go

    grant connect sql to [BUILTIN\Administrators]
go

use TestInflow
go

use master
go

grant connect sql to [NT AUTHORITY\NETWORK SERVICE]
go

use TestInflow
go

use master
go

grant alter any event session, connect any database, connect sql, view any definition, view server state to [NT AUTHORITY\SYSTEM]
go

use TestInflow
go

use master
go

grant view any database to [public]
go

use TestInflow
go

use master
go

grant connect sql to sa
go

use TestInflow
go

