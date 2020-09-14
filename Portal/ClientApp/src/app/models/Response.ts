namespace Models {
  export class Response<T> {
    public success: boolean;
    public message: string;
    public data: T;
    public rowCount: number;

    constructor() {
      this.success = false;
      this.message = "";
      this.data = null;
      this.rowCount = 0;
    }
  }
}
