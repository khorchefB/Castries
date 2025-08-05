'use server';
import { Auction, Bid, PagedResult } from "@/types/index";
import { fetchWrapper } from "../lib/fetchWrapper";
import { FieldValues } from "react-hook-form";


export async function getData(query: string): Promise<PagedResult<Auction>> {
    const res = await fetch(`http://localhost:6001/search${query}`);
    if (!res.ok) { 
        throw new Error('Failed to fetch data');
    }
    return res.json();
}

export async function updateAuctionTest(): Promise<{status: number, message: string}> {
    const data = {
        mileage: Math.floor(Math.random() * 10000) + 1
    } 
    return fetchWrapper.put('auctions/afbee524-5972-4075-8800-7d1f9d7b0a0c', data);
}

export async function createAuction(data: FieldValues){
    return fetchWrapper.post('auctions', data);
}

export async function updateAuction(data: FieldValues, id: string) {
    return await fetchWrapper.put(`auctions/${id}`, data);
}

export async function deleteAuction(id: string) {
    return await fetchWrapper.del(`auctions/${id}`);
}

export async function getDetailedViewData(id: string): Promise<Auction> {
    return fetchWrapper.get(`auctions/${id}`);
}

export async function getBidsForAuction(id: string): Promise<Bid[]> {
    return fetchWrapper.get(`bids/${id}`);
}

export async function placeBidForAuction(auctionId: string, amount: number) {
    return fetchWrapper.post(`bids?auctionId=${auctionId}&amount=${amount}`, {});
}