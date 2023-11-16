-- Criação da tabela de usuários
CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    username VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL
);

-- Criação da tabela de papéis (roles)
CREATE TABLE roles (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL UNIQUE
);

-- Criação da tabela de grupos
CREATE TABLE groups (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL UNIQUE
);

-- Criação da tabela de módulos
CREATE TABLE modules (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL UNIQUE,
    description TEXT,
    parent_module_id INTEGER REFERENCES modules(id) -- Auto-relacionamento para módulos filhos
);

-- Criação da tabela de permissões
CREATE TABLE permissions (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL UNIQUE,
    description TEXT
);

-- Tabela associativa entre usuários e papéis
CREATE TABLE user_roles (
    user_id INTEGER REFERENCES users(id),
    role_id INTEGER REFERENCES roles(id),
    PRIMARY KEY (user_id, role_id)
);

-- Tabela associativa entre usuários e grupos
CREATE TABLE user_groups (
    user_id INTEGER REFERENCES users(id),
    group_id INTEGER REFERENCES groups(id),
    PRIMARY KEY (user_id, group_id)
);

-- Tabela associativa entre grupos e módulos
CREATE TABLE group_modules (
    group_id INTEGER REFERENCES groups(id),
    module_id INTEGER REFERENCES modules(id),
    PRIMARY KEY (group_id, module_id)
);

-- Tabela associativa entre grupos e permissões
CREATE TABLE group_permissions (
    group_id INTEGER REFERENCES groups(id),
    permission_id INTEGER REFERENCES permissions(id),
    PRIMARY KEY (group_id, permission_id)
);

-- Tabela associativa entre módulos e permissões
CREATE TABLE module_permissions (
    module_id INTEGER REFERENCES modules(id),
    permission_id INTEGER REFERENCES permissions(id),
    PRIMARY KEY (module_id, permission_id)
);
