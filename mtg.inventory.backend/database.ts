import * as mysql from 'mysql';
import { Logger } from './logger';

export class Database {
    connection: any;
    config:{host:string, user: string, password: string, database:string};

    constructor(){
        // TODO: Get Config from env
        this.config = {
            host: 'localhost',
            user: 'root',
            password: 'my-secret-pw',
            database: 'mtg'
        };

        // Create Database if not existing / connecting with database mysql
        this.connection = mysql.createConnection({...{database:'mysql'},...this.config});
        this.connection.query('CREATE DATABASE IF NOT EXISTS ' + this.config.database, (err, rows, fields) => {
            if (err) { Logger.error(err, "mysql-create-db") ; throw err}
        });
        this.connection.end();

        // Startup MySQL Client with selected DB
        this.connection = mysql.createConnection(this.config);
        this.connection.connect()

        // Running migrations
        this.migrate();
    }


    private migrate(){
        Logger.info('running migrations', 'mysql-migration');

        /*************** 
         *  2022-12-18
        ***************/
         Logger.info('running 2022-12-18', 'mysql-migration');
       
        // Create Table users
        Logger.info('create users table', 'mysql-migration');
        this.connection.query(`
            CREATE TABLE IF NOT EXISTS \`users\` (
                \`id\` INT NOT NULL AUTO_INCREMENT,
                \`username\` VARCHAR(512) NOT NULL,
                \`email\` VARCHAR(512) NOT NULL,
                \`password\` VARCHAR(512) NOT NULL,
                \`displayName\` VARCHAR(512) NOT NULL,
                PRIMARY KEY (\`id\`)
            )
        `, (err, rows, fields) => {
            if (err) { Logger.error(err, "mysql") ; throw err}
        });
       
        // Create Table collections
        Logger.info('create collections table', 'mysql-migration');
        this.connection.query(`
            CREATE TABLE IF NOT EXISTS \`collections\` (
                \`id\` INT NOT NULL AUTO_INCREMENT,
                \`displayName\` VARCHAR(512) NOT NULL,
                \`description\` VARCHAR(1024) NULL,
                PRIMARY KEY (\`id\`)
            )
        `, (err, rows, fields) => {
            if (err) { Logger.error(err, "mysql") ; throw err}
        });
       
        // Create Table folders
        Logger.info('create folders table', 'mysql-migration');
        this.connection.query(`
            CREATE TABLE IF NOT EXISTS \`folders\` (
                \`id\` INT NOT NULL AUTO_INCREMENT,
                \`displayName\` VARCHAR(512) NOT NULL,
                \`description\` VARCHAR(1024) NULL,
                \`collectionID\` INT UNSIGNED NOT NULL,
                PRIMARY KEY (\`id\`),
                CONSTRAINT \`fk_collection\` FOREIGN KEY (\`collectionID\`) REFERENCES \`collections\` (\`id\`) ON UPDATE NO ACTION ON DELETE CASCADE
            )
        `, (err, rows, fields) => {
            if (err) { Logger.error(err, "mysql") ; throw err}
        });

        // Create Table cards
        Logger.info('create cards table', 'mysql-migration');
        this.connection.query(`
            CREATE TABLE IF NOT EXISTS \`cards\` (
                \`id\` INT NOT NULL AUTO_INCREMENT,
                \`name\` VARCHAR(512) NOT NULL,
                \`skyfallID\` VARCHAR(512) NOT NULL,
                \`folderID\` INT UNSIGNED NOT NULL,
                \`quantity\` INT NOT NULL DEFAULT '0',
                PRIMARY KEY (\`id\`),
                CONSTRAINT \`fk_folder\` FOREIGN KEY (\`folderID\`) REFERENCES \`folders\` (\`id\`) ON UPDATE NO ACTION ON DELETE CASCADE
            )
        `, (err, rows, fields) => {
            if (err) { Logger.error(err, "mysql") ; throw err}
        });
       
        // Create Table decks
        Logger.info('create decks table', 'mysql-migration');
        this.connection.query(`
            CREATE TABLE IF NOT EXISTS \`decks\` (
                \`id\` INT NOT NULL AUTO_INCREMENT,
                \`displayName\` VARCHAR(512) NOT NULL,
                \`description\` VARCHAR(1024) NULL,
                PRIMARY KEY (\`id\`)
            )
        `, (err, rows, fields) => {
            if (err) { Logger.error(err, "mysql") ; throw err}
        });
       
        // Create Table card2deck
        Logger.info('create card2deck table', 'mysql-migration');
        this.connection.query(`
            CREATE TABLE IF NOT EXISTS \`card2deck\` (
                \`id\` INT NOT NULL AUTO_INCREMENT,
                \`cardID\` INT UNSIGNED NOT NULL,
                CONSTRAINT \`fk_cards\` FOREIGN KEY (\`cardID\`) REFERENCES \`cards\` (\`id\`) ON UPDATE NO ACTION ON DELETE CASCADE,
                \`deckID\` INT UNSIGNED NOT NULL,
                CONSTRAINT \`fk_decks\` FOREIGN KEY (\`deckID\`) REFERENCES \`decks\` (\`id\`) ON UPDATE NO ACTION ON DELETE CASCADE,
                \`quantity\` INT NOT NULL DEFAULT '0',
                PRIMARY KEY (\`id\`)
            )
        `, (err, rows, fields) => {
            if (err) { Logger.error(err, "mysql") ; throw err}
        });
    }
}