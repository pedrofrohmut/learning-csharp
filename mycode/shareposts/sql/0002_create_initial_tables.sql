create extension if not exists "uuid-ossp";

create table if not exists users (
    name varchar(128) not null,
    email varchar(256) not null,
    password_hash varchar(128) not null,
    phone varchar(32) null,

    created_at timestamp default now(),
    updated_at timestamp default now(),

    id uuid default uuid_generate_v4(),

    primary key(id)
);

create table if not exists posts (
    title varchar(255) not null,
    body varchar(512) not null,

    created_at timestamp default now(),
    updated_at timestamp default now(),

    id uuid default uuid_generate_v4(),
    user_id uuid not null,

    primary key(id),
    constraint fk_posts_users foreign key (user_id) references users(id) on delete cascade
);
