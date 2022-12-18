export class Logger {
    static info(message:string, module:string = "main"){
        this.write("info", message,module);
    }

    static error(message:string, module:string = "main"){
        this.write("error", message,module);
    }

    private static write(level:string,message:string,module:string){
        console.log( Date.now() + " - " + level.toUpperCase() + " [" + module.toLowerCase() +"]: " + message  );
    }
}