# SharpLDAPSearch
C# .NET Assembly to perform LDAP Queries


## Usage
Positional arguments for the ldap query and comma-separated attributes to return from the query

If no attributes are specified, all attributes are returned for each object in the query's results 
```
execute-assembly /path/to/SharpLDAPSearch.exe "ldap query" "attributes,to,return"
```

## Example
```
SharpLDAPSearch.exe "(&(objectClass=user)(cn=*svc*))" "samaccountname"
```
### Output
```
svc-admin
svc-ops
spsvcuser
```
